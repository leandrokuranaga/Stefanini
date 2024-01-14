namespace Stefanini.Infra.Data
{
    public interface IUnitOfWork
    {
        DatabaseContext Context { get; }
        Task CommitAsync();
        Task CommitWithIdentityInsertAsync(string table);
    }
}
