using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Infra.Data;
using Stefanini.Infra.Data.Repository;

namespace Stefanini.Infra.Repository
{
    public class CityRepository : BaseRepository<DatabaseContext, CityDomain>, ICityRepository
    {
        protected DatabaseContext _context;

        public CityRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<CityDomain> AddCity(CityDomain city)
        {
            _context.City.AddAsync(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task<IEnumerable<CityDomain>> GetAllCities()
        {

            var cities = await _context
                                .City
                                .AsNoTracking()
                                .ToListAsync();

            return cities;
        }

        public async Task<CityDomain> GetCityById(int id)
        {
            var city = await _context
                                .City
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            return city;
        }

    }
}
