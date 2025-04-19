using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Common;
using Stefanini.Application.Validators;
using Stefanini.Domain.CityAggregate.ValueObjects;
using Stefanini.Domain.SeedWork.Enums;
using Stefanini.Domain.SeedWork.Notification;
using Stefanini.Infra.Services;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.SeedWork;

namespace Stefanini.Application.Person.Services
{
    public class PersonService(
        IPersonRepository personRepository,
        INotification notification,
        ICacheService cache,
        ICityRepository cityRepository) : BaseService(notification), IPersonService
    {
        public Task<BasePaginatedResponse<List<PersonResponse>>> GetPaginatedAsync(int page, int pageSize) =>
            ExecuteAsync<BasePaginatedResponse<List<PersonResponse>>>(async () =>
            {
                var cacheKey = $"{EnumCacheTags.Person}:Page:{page}:Size:{pageSize}";

                Func<Task<BasePaginatedResponse<List<PersonResponse>>>> factory = async () =>
                {
                    var (people, totalItems) = await personRepository.GetPaginatedAsync(page, pageSize);

                    return new BasePaginatedResponse<List<PersonResponse>>
                    {
                        Data = people.Select(p => (PersonResponse)p).ToList(),
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItens = totalItems
                    };
                };

                return await cache.GetOrSetAsync(cacheKey, factory);
            });

        public Task<PersonResponse> GetAsync(int id) => ExecuteAsync(async () =>
        {
            var cacheKey = $"{EnumCacheTags.Person}:Id:{id}";

            Func<Task<PersonResponse>> factory = async () =>
            {
                var person = await personRepository.GetByIdAsync(id, false);

                if (person is null)
                {
                    notification.AddNotification("Get Person", "Person does not exist", NotificationModel.ENotificationType.BadRequestError);
                    return new PersonResponse();
                }

                return (PersonResponse)person;
            };

            return await cache.GetOrSetAsync(cacheKey, factory);
        });

        public Task<PersonResponse> CreateAsync(PersonRequest request) => ExecuteAsync(async () =>
        {
            Validate(request, new PersonRequestValidator());

            var existingPeople = await personRepository.GetAsync(x => x.CPF.Value == request.CPF);
            if (existingPeople.Any())
            {
                notification.AddNotification("Create Person", "Person already exists", NotificationModel.ENotificationType.BadRequestError);
                return new PersonResponse();
            }
            if (request.CityId > 0)
            {
                var city = await cityRepository.GetByIdAsync(request.CityId ?? 1, false);
                if (city is null)
                {
                    notification.AddNotification("Create Person", "City does not exist", NotificationModel.ENotificationType.BadRequestError);
                    return new PersonResponse();
                }
            }

            var personDomain = (Domain.CityAggregate.Entity.Person)request;
            await personRepository.InsertOrUpdateAsync(personDomain);
            await personRepository.SaveChangesAsync();

            await ClearPersonCache(personDomain.Id);
            return (PersonResponse)personDomain;
        });

        public Task<PersonResponse> UpdateAsync(int id, PersonRequest request) => ExecuteAsync(async () =>
        {
            Validate(request, new PersonRequestValidator());

            var personWithSameCPF = await personRepository.GetOneNoTracking(x => x.CPF.Value == request.CPF);
            if (personWithSameCPF is not null && personWithSameCPF.Id != id)
            {
                notification.AddNotification("Update Person", "Person already exists", NotificationModel.ENotificationType.BadRequestError);
                return new PersonResponse();
            }

            if (request.CityId > 0)
            {
                var city = await cityRepository.GetByIdAsync(request.CityId ?? 1, false);
                if (city is null)
                {
                    notification.AddNotification("Create Person", "City does not exist", NotificationModel.ENotificationType.BadRequestError);
                    return new PersonResponse();
                }
            }

            var personFetched = await personRepository.GetByIdAsync(id, noTracking: false);
            if (personFetched is null)
            {
                notification.AddNotification("Update Person", "Person not found", NotificationModel.ENotificationType.NotFound);
                return new PersonResponse();
            }

            UpdatePersonProperties(personFetched, request);
            await personRepository.InsertOrUpdateAsync(personFetched);
            await personRepository.SaveChangesAsync();

            await ClearPersonCache(id);
            return (PersonResponse)personFetched;
        });

        public Task<BaseResponse<object>> DeleteAsync(int id) => ExecuteAsync(async () =>
        {
            var person = await personRepository.GetByIdAsync(id, false);
            if (person is null)
            {
                notification.AddNotification("Delete Person", "Person not found", NotificationModel.ENotificationType.NotFound);
                var errorNotification = new NotificationModel
                {
                    NotificationType = NotificationModel.ENotificationType.NotFound
                };
                errorNotification.AddMessage("Delete Person", "Person not found");

                return BaseResponse<object>.Fail(errorNotification);
            }

            await personRepository.DeleteAsync(person);
            await personRepository.SaveChangesAsync();

            await ClearPersonCache(id);
            return BaseResponse<object>.Ok(new { message = "Person deleted successfully" });
        });

        private static void UpdatePersonProperties(Domain.CityAggregate.Entity.Person personDb, PersonRequest request)
        {
            personDb.Name = new Name(request.Name);
            personDb.CPF = new CPF(request.CPF);
            personDb.Age = new Age(request.Age);
            personDb.CityId = request.CityId;
        }

        private async Task ClearPersonCache(int personId)
        {
            await cache.RemoveAsync(
                $"{EnumCacheTags.Person}:Id:{personId}",
                $"{EnumCacheTags.Person}:Page:*"
            );
        }
    }
}
