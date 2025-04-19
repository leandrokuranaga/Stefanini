using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Infra.Data;
using Stefanini.Infra.Data.Repository.Base;

namespace Stefanini.Infra.Data.Repository
{
    public class CityRepository(Context context) : BaseRepository<City>(context), ICityRepository
    {
        public async Task<(List<City>, int totalItems)> GetPaginatedAsync(int page, int pageSize)
        {
            var query = context.City.AsQueryable();

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
