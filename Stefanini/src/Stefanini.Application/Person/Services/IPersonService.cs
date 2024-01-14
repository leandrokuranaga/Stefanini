using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Application.Person.Services
{
    public interface IPersonService
    {
        public Task<List<PersonResponse>> GetAllPeople();
        public Task<PersonResponse> GetPersonById(int id);
        public Task AddPerson(PersonRequest person);
        public Task<bool> UpdatePerson(PersonRequest person, int id);
        public Task<bool> DeletePerson(int id);
    }
}
