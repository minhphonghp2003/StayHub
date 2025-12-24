using System.Net;

namespace Shared.Response
{
    public class Response<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }

    }
    public class BaseResponse<T> : Response<T>
    {
        public T? Data { get; set; }
    }
    public class PaginatedResponse<T> : Response<T>
    {
        public List<T>? Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / Math.Max( PageSize,1));
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
    }
}
