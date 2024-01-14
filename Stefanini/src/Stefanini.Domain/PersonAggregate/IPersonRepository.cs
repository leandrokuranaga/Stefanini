using Stefanini.Domain.CityAggregate;

namespace Stefanini.Domain.PersonAggregate
{
    public interface IPersonRepository : IBaseRepository<PersonDomain>
    {
        Task<bool> DeletePersonById(int id);
        Task<bool> UpdatePerson(PersonDomain person);
        Task<CityDomain> GetCityByPerson(int idCity);
    }
}
