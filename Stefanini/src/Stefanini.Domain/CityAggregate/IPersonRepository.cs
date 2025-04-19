using Stefanini.Domain.CityAggregate.Entity;
using Stefanini.Infra.Data;

namespace Stefanini.Domain.CityAggregate
{
    public interface IPersonRepository : IBaseRepository<Person>, IUnitOfWork
    {
        Task<(List<Person>, int totalItems)> GetPaginatedAsync(int page, int pageSize);
    }
}
