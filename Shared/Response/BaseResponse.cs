using System.Net;

namespace Shared.Response
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? Error { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
