using Microsoft.AspNetCore.Mvc;
using File.Infrastructure.Service.File;
using Shared.Model.S3;
using Microsoft.AspNetCore.Http;

namespace File.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IMinIOService _minioService;

        public FileController(IMinIOService minioService)
        {
            _minioService = minioService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] List<IFormFile> files, [FromForm] string? bucketName, [FromForm] bool isPublicBucket = false)
        {
            if (files == null || files.Count == 0)
                return BadRequest("No files provided.");

            var bucket = string.IsNullOrWhiteSpace(bucketName) ? "default" : bucketName!;
            var result = await _minioService.UploadFileAsync(files, bucket, isPublicBucket);
            return Ok(result);
        }

        [HttpGet("url")]
        public async Task<IActionResult> GetUrl([FromQuery] string bucketName, [FromQuery] string objectName, [FromQuery] int expire = 60)
        {
            if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(objectName))
                return BadRequest("bucketName and objectName are required.");

            var url = await _minioService.GetShareLinkAsync(bucketName, objectName, expire);
            return Ok(new { url });
        }
    }
}
