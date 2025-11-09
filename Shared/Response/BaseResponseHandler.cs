using System.Net;

namespace Shared.Response
{
    public class BaseResponseHandler
    {
        public BaseResponse<T> Success<T>(T data, String? message = null, HttpStatusCode code = HttpStatusCode.OK)
        {
            return new BaseResponse<T>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = code,

            };
        }
        public BaseResponse<T> Failure<T>(String message, HttpStatusCode code)
        {
            return new BaseResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = code,

            };
        }
        public PaginatedResponse<T> SuccessPaginated<T>(List<T> data, int totalCount, int pageSize, int currentPage, String? message = null, HttpStatusCode code = HttpStatusCode.OK)
        {
            return new PaginatedResponse<T>
            {
                Success = true,
                Data = data,
                CurrentPage = currentPage,
                TotalCount = totalCount,
                PageSize = pageSize,
                Message = message,
                StatusCode = code,

            };
        }
        public PaginatedResponse<T> FailurePaginated<T>(String message, HttpStatusCode code)
        {
            return new PaginatedResponse<T>
            {
                Success = false,
                Message = message,
                StatusCode = code,

            };
        }
    }
}
