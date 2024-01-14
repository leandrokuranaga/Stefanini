using Microsoft.EntityFrameworkCore;

namespace Stefanini.Infra.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public DatabaseContext Context { get; set; }

        public UnitOfWork(DatabaseContext context)
        {
            Context = context;
        }

        public async Task CommitAsync()
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task CommitWithIdentityInsertAsync(string table)
        {
            using (var transaction = await Context.Database.BeginTransactionAsync())
            {
                try
                {
                    Context.Database.ExecuteSqlRaw($"SET IDENTITY_INSERT {table} ON;");
                    await Context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
