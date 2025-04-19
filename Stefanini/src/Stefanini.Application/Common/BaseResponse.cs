using Stefanini.Domain.SeedWork;

namespace Stefanini.Application.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public NotificationModel? Error { get; set; }

        public static BaseResponse<T> Ok(T data) =>
            new()
            { Success = true, Data = data };

        public static BaseResponse<T> Fail(NotificationModel error) =>
            new()
            { Success = false, Error = error };
    }
}

