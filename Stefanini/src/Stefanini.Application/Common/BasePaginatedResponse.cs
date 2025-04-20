using Stefanini.Domain.SeedWork;

public class BasePaginatedResponse<T>
{
    public bool Success { get; set; } = true;
    public T? Data { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalItens { get; set; }
    public decimal TotalPages => Math.Ceiling((decimal)TotalItens / PageSize);
    public NotificationModel? Error { get; set; }
}
