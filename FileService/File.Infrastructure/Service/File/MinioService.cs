using Microsoft.AspNetCore.Http;
using Minio;
using Minio.DataModel.Args;
using Minio.Exceptions;
using Shared.Model.S3;
using System.Reflection;

namespace File.Infrastructure.Service.File
{
    public interface IMinIOService
    {
        Task<List<MinioCloudModel>> UploadFileAsync(List<IFormFile> files, string bucketName, bool isPublicBucket = false);
        Task<string> GetShareLinkAsync(string bucketName, string objectName, int expireTime);
    }
    public class MinioService(IMinioClient _minioClient) : IMinIOService
    {
        public async Task<List<MinioCloudModel>> UploadFileAsync(List<IFormFile> files, string bucketName, bool isPublicBucket = false)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));
            if (string.IsNullOrWhiteSpace(bucketName))
                throw new ArgumentException("Bucket name is required", nameof(bucketName));

            var result = new List<MinioCloudModel>();

            // Ensure bucket exists
            try
            {
                var existsArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool exists = await _minioClient.BucketExistsAsync(existsArgs).ConfigureAwait(false);
                if (!exists)
                {
                    var makeArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await _minioClient.MakeBucketAsync(makeArgs).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                // Re-throw with context
                throw new InvalidOperationException($"Failed ensuring bucket '{bucketName}' exists.", ex);
            }

            foreach (var file in files)
            {
                if (file == null || file.Length == 0)
                    continue;

                var originalName = Path.GetFileName(file.FileName);
                var objectName = $"{Guid.NewGuid():N}_{originalName}";

                try
                {
                    using var stream = file.OpenReadStream();
                    var putArgs = new PutObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectName)
                        .WithStreamData(stream)
                        .WithObjectSize(stream.Length);

                    if (!string.IsNullOrWhiteSpace(file.ContentType))
                        putArgs = putArgs.WithContentType(file.ContentType);

                    await _minioClient.PutObjectAsync(putArgs).ConfigureAwait(false);

                    // Build a best-effort URL (not guaranteed to be public). Use s3:// as fallback.
                    string url = $"s3://{bucketName}/{objectName}";

                    // If MinIO client exposes endpoint via property, try to use it to create http url
                    var endpointProp = _minioClient.GetType().GetProperty("Endpoint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    if (endpointProp != null)
                    {
                        var endpointValue = endpointProp.GetValue(_minioClient)?.ToString();
                        if (!string.IsNullOrWhiteSpace(endpointValue))
                        {
                            // Ensure scheme present
                            if (!endpointValue.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                                endpointValue = "http://" + endpointValue.TrimEnd('/');

                            url = $"{endpointValue.TrimEnd('/')}/{bucketName}/{objectName}";
                        }
                    }

                    // Create MinioCloudModel instance and populate known fields via reflection so we don't
                    // rely on an exact shape of the shared model. This will set common properties if they exist.
                    var modelObj = Activator.CreateInstance(typeof(MinioCloudModel));
                    if (modelObj != null)
                    {
                        void TrySet(string propName, object val)
                        {
                            var p = modelObj!.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);
                            if (p != null && p.CanWrite)
                            {
                                try
                                { p.SetValue(modelObj, val); }
                                catch { /* ignore set failure */ }
                            }
                        }

                        TrySet("BucketName", bucketName);
                        TrySet("ObjectName", objectName);
                        TrySet("Url", url);
                        TrySet("Size", stream.Length);
                        TrySet("ContentType", file.ContentType ?? "application/octet-stream");
                        TrySet("IsPublic", isPublicBucket);
                    }

                    result.Add((MinioCloudModel)modelObj!);
                }
                catch (MinioException mex)
                {
                    // continue on other files but bubble a helpful exception in list if needed
                    throw new InvalidOperationException($"Failed uploading file '{originalName}' to bucket '{bucketName}'.", mex);
                }
            }

            return result;
        }

        
        public async Task<string> GetShareLinkAsync(string bucketName, string objectName, int expireTime)
        {
            if (string.IsNullOrWhiteSpace(bucketName))
                throw new ArgumentException("Bucket name is required", nameof(bucketName));
            if (string.IsNullOrWhiteSpace(objectName))
                throw new ArgumentException("Object name is required", nameof(objectName));
            if (expireTime <= 0)
                expireTime = 60; // default 60s

            try
            {
                // Try the newer Args-based API first
                try
                {
                    var presignedArgs = new PresignedGetObjectArgs()
                        .WithBucket(bucketName)
                        .WithObject(objectName)
                        .WithExpiry(expireTime);

                    var presigned = await _minioClient.PresignedGetObjectAsync(presignedArgs).ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(presigned))
                        return presigned;
                }
                catch (MissingMethodException) { /* Fall through to older overload attempt */ }
                catch (TargetInvocationException) { /* Fall through */ }
                catch (Exception) { /* If it fails, we try an alternate approach below */ }

                // Fallback: try older overloads (some SDK versions have PresignedGetObjectAsync(bucket, object, expiry))
                var method = _minioClient.GetType().GetMethod("PresignedGetObjectAsync", new[] { typeof(string), typeof(string), typeof(int) });
                if (method != null)
                {
                    var taskObj = (Task<string>)method.Invoke(_minioClient, new object[] { bucketName, objectName, expireTime })!;
                    var url = await taskObj.ConfigureAwait(false);
                    if (!string.IsNullOrWhiteSpace(url))
                        return url;
                }

                // As a last resort, return an s3:// style link (not a presigned HTTP URL)
                var endpointProp = _minioClient.GetType().GetProperty("Endpoint", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (endpointProp != null)
                {
                    var endpointValue = endpointProp.GetValue(_minioClient)?.ToString();
                    if (!string.IsNullOrWhiteSpace(endpointValue))
                    {
                        if (!endpointValue.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                            endpointValue = "http://" + endpointValue.TrimEnd('/');

                        return $"{endpointValue.TrimEnd('/')}/{bucketName}/{objectName}";
                    }
                }

                return $"s3://{bucketName}/{objectName}";
            }
            catch (MinioException mex)
            {
                throw new InvalidOperationException($"Failed to generate presigned URL for '{objectName}' in '{bucketName}'.", mex);
            }
        }
    }
}
