using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.CityAggregate.Entity;
using Stefanini.Infra.Data.Repository.Base;

namespace Stefanini.Infra.Data.Repository
{
    public class PersonRepository(Context context) : BaseRepository<Person>(context), IPersonRepository
    {
        public async Task<(List<Person>, int totalItems)> GetPaginatedAsync(int page, int pageSize)
        {
            var query = context.Person
                  .Include(p => p.City)
                  .AsNoTracking();
            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalItems);
        }
    }
}
