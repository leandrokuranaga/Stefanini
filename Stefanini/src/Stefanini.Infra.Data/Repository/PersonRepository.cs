using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.PersonAggregate;
using Stefanini.Infra.Data;
using Stefanini.Infra.Data.Repository;

namespace Stefanini.Infra.Repository
{
    public class PersonRepository : BaseRepository <DatabaseContext, PersonDomain>, IPersonRepository
    {
        protected DatabaseContext _context;

        public PersonRepository(DatabaseContext context) : base(context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> DeletePersonById(int id)
        {

            var person = await _context
                                .Person
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return false;

            var x = _context.Remove(new PersonDomain { Id = id });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CityDomain> GetCityByPerson(int idCity)
        {
            var city = await _context
                                .City
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == idCity);

            return city;
        }

        public async Task<bool> UpdatePerson(PersonDomain person)
        {
            var updated = await _context
                                .Person
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == person.Id);

            if (updated == null)
                return false;

            _context.Person.Update(person);
            await _context.SaveChangesAsync();

            return true;
        }

        
    }
}
