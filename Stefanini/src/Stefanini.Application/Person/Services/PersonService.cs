using Stefanini.Application.Common;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Validators;
using Stefanini.Domain.PersonAggregate;
using Stefanini.Domain.SeedWork.Notification;

namespace Stefanini.Application.Person.Services
{
    public class PersonService(IPersonRepository personRepository, INotification notification) : BaseService(notification), IPersonService
    {
        private readonly INotification _notification = notification;

        private readonly IPersonRepository _personRepository = personRepository;

        public async Task AddPerson(PersonRequest person)
        {
            try
            {
                Validate(person, new PersonRequestValidator());
                var personDomain = (PersonDomain)person;

                await _personRepository.InsertOrUpdateAsync(personDomain);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> DeletePerson(int id)
        {
            var deletedPerson = _personRepository.DeletePersonById(id);
            return deletedPerson;
        }

        public async Task<List<PersonResponse>> GetAllPeople()
        {
            var people = await _personRepository.GetAllAsync();

            var results = people.Select(person => (PersonResponse)person).ToList();

            return results;
        }

        public async Task<PersonResponse> GetPersonById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id, false);

            if (person is null)
                 return null;

            var personResponse = (PersonResponse)person;

            return personResponse;
        }

        public async Task<bool> UpdatePerson(PersonRequest person, int id)
        {
            try
            {
                Validate(person, new PersonRequestValidator());
                var personDomain = (PersonDomain)person;

                await _personRepository.UpdateAsync(personDomain);

                return true;
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async Task<bool> AddPersonToCity(int personId, int cityId)
        {
            var updatedPerson = await _personRepository.AddPersonToCity(personId, cityId);

            return updatedPerson;
        }

    }
}
