namespace Stefanini.Application.Common
{
    public class BasePaginatedResponse<T> : BaseResponse<T>
    {
        public decimal TotalPages => Math.Ceiling((decimal)TotalItens / PageSize);
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItens { get; set; }
    }
}
