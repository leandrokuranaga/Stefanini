using Microsoft.Extensions.Caching.Memory;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.Common;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Validators;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.CityAggregate.ValueObjects;
using Stefanini.Domain.SeedWork;
using Stefanini.Domain.SeedWork.Enums;
using Stefanini.Domain.SeedWork.Notification;

namespace Stefanini.Application.Person.Services
{
    public class PersonService(IPersonRepository personRepository, INotification notification, IMemoryCache cache) : BaseService(notification), IPersonService
    {
        public Task<BasePaginatedResponse<List<PersonResponse>>> GetPaginatedAsync(int page, int pageSize) =>
            ExecuteAsync<BasePaginatedResponse<List<PersonResponse>>>(async () =>
            {
                var cacheKey = $"{EnumCacheTags.Person}:Page:{page}:Size:{pageSize}";

                if (cache.TryGetValue(cacheKey, out BasePaginatedResponse<List<PersonResponse>> cached))
                    return cached;

                var (people, totalItems) = await personRepository.GetPaginatedAsync(page, pageSize);

                var result = new BasePaginatedResponse<List<PersonResponse>>
                {
                    Data = people.Select(p => (PersonResponse)p).ToList(),
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItens = totalItems
                };

                cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

                return result;
            });

        public Task<PersonResponse> GetAsync(int id) => ExecuteAsync(async () =>
        {
            var cacheKey = $"{EnumCacheTags.Person}:Id:{id}";

            if (cache.TryGetValue(cacheKey, out PersonResponse cached))
                return cached;

            var person = await personRepository.GetByIdAsync(id, false);

            if (person is null)
            {
                notification.AddNotification("Get Person", "Person does not exists", NotificationModel.ENotificationType.BadRequestError);
                return new PersonResponse();
            }

            var personResponse = (PersonResponse)person;

            cache.Set(cacheKey, personResponse, TimeSpan.FromMinutes(5));

            return personResponse;
        });


        public Task<PersonResponse> CreateAsync(PersonRequest person) => ExecuteAsync(async () =>
        {
            Validate(person, new PersonRequestValidator());

            var personId = await personRepository.GetAsync(x => x.CPF.Value == person.CPF);

            if (personId.Count() > 0)
            {
                notification.AddNotification("Create User", "User already exists", NotificationModel.ENotificationType.BadRequestError);
                return new PersonResponse();
            }

            var personDomain = (Domain.CityAggregate.Entity.Person)person;

            await personRepository.InsertOrUpdateAsync(personDomain);
            await personRepository.SaveChangesAsync();

            ClearPersonCache(personDomain.Id);

            var personResponse = (PersonResponse)personDomain;
            return (personResponse);
        });

        public Task<PersonResponse> UpdateAsync(int id, PersonRequest person) => ExecuteAsync(async () =>
        {
            Validate(person, new PersonRequestValidator());

            var personExists = await personRepository.GetOneNoTracking(x => x.CPF.Value == person.CPF);

            if (personExists != null && personExists.Id != id)
            {
                notification.AddNotification("Update Person", "User already exists", NotificationModel.ENotificationType.BadRequestError);
                return new PersonResponse();
            }

            var personFetched = await personRepository.GetByIdAsync(id, noTracking: false);

            if (personFetched == null)
            {
                notification.AddNotification("Update User", "User not found", NotificationModel.ENotificationType.NotFound);
            }

            UpdatePersonProperties(personFetched, person);

            await personRepository.InsertOrUpdateAsync(personFetched);
            await personRepository.SaveChangesAsync();

            ClearPersonCache(personFetched.Id);

            var personResponse = (PersonResponse)personFetched;

            return personResponse;
        });

        private void UpdatePersonProperties(Domain.CityAggregate.Entity.Person personDb, PersonRequest person)
        {
            personDb.Name = new Name(person.Name);
            personDb.CPF = new CPF(person.CPF);
            personDb.Age = new Age(person.Age);
            personDb.CityId = person.CityId;
        }

        public Task<BaseResponse<object>> DeleteAsync(int id) => ExecuteAsync(async () =>
        {
            var person = await personRepository.GetByIdAsync(id, false);

            if (person == null)
            {
                notification.AddNotification("Delete User", "User not found", NotificationModel.ENotificationType.NotFound);
                return BaseResponse<object>.Fail(_notification.NotificationModel);
            }

            await personRepository.DeleteAsync(person);
            await personRepository.SaveChangesAsync();

            ClearPersonCache(person.Id);

            return BaseResponse<object>.Ok(null);
        });

        private void ClearPersonCache(int personId)
        {
            cache.Remove($"{EnumCacheTags.Person}:Id:{personId}");

            for (int page = 1; page <= 10; page++)
            {
                for (int size = 5; size <= 50; size += 5)
                {
                    cache.Remove($"{EnumCacheTags.Person}:Page:{page}:Size:{size}");
                }
            }
        }

    }
}
