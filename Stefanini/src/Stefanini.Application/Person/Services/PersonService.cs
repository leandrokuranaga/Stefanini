using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Application.Person.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task AddPerson(PersonRequest person)
        {
            try
            {
                PersonDomain personDomain = new PersonDomain
                { 
                    Age = person.Age,
                    Name = person.Name,
                    CPF = person.CPF,
                };

                await _personRepository.InsertOrUpdateAsync(personDomain);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

            List<PersonResponse> results = new();

            foreach (var person in people)
            {
                PersonResponse response = new()
                { 
                    Id = person.Id,
                    Name = person.Name,
                    Age = person.Age,
                    Cpf = person.CPF,
                };
                results.Add(response);
            }

            return results;

        }

        public async Task<PersonResponse> GetPersonById(int id)
        {
            var person = await _personRepository.GetByIdAsync(id, false);

            PersonResponse personResponse = new();

            if (person != null)
            {
                personResponse.Id = person.Id;
                personResponse.Name = person.Name;
                personResponse.Age = person.Age;
                personResponse.Cpf = person.CPF;             
            }

            if (person == null)
            {
                return null;
            }

            return personResponse;
        }

        public async Task<bool> UpdatePerson(PersonRequest person, int id)
        {
            PersonDomain personDomain = new()
            {
                Id = id,
                Name = person.Name,
                Age = person.Age,
                CPF = person.CPF
            };

            await _personRepository.UpdateAsync(personDomain);

            return true; 
        }

    }
}
