using Stefanini.Infra.Data;

namespace Stefanini.Domain.CityAggregate
{
    public interface ICityRepository : IBaseRepository<City>, IUnitOfWork
    {
        Task<(List<City>, int totalItems)> GetPaginatedAsync(int page, int pageSize);
    }
}
