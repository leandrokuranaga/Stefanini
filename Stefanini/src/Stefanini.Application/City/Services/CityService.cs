using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.Common;
using Stefanini.Application.Validators;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.CityAggregate.ValueObjects;
using Stefanini.Domain.SeedWork;
using Stefanini.Domain.SeedWork.Enums;
using Stefanini.Domain.SeedWork.Notification;
using Stefanini.Infra.Services;

namespace Stefanini.Application.City.Services
{
    public class CityService(
        ICityRepository cityRepository,
        INotification notification,
        ICacheService cache) : BaseService(notification), ICityService
    {
        public Task<BasePaginatedResponse<List<CityResponse>>> GetPaginatedAsync(int page, int pageSize) =>
            ExecuteAsync(async () =>
            {
                var cacheKey = $"{EnumCacheTags.City}:Page:{page}:Size:{pageSize}";

                Func<Task<BasePaginatedResponse<List<CityResponse>>>> factory = async () =>
                {
                    var (cities, totalItems) = await cityRepository.GetPaginatedAsync(page, pageSize);

                    return new BasePaginatedResponse<List<CityResponse>>
                    {
                        Data = cities.Select(c => (CityResponse)c).ToList(),
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItens = totalItems,
                        Success = true
                    };
                };

                return await cache.GetOrSetAsync(cacheKey, factory);
            });


        public Task<CityResponse> GetAsync(int id) => ExecuteAsync(async () =>
        {
            var cacheKey = $"{EnumCacheTags.City}:Id:{id}";

            Func<Task<CityResponse>> factory = async () =>
            {
                var city = await cityRepository.GetByIdAsync(id, false);

                if (city is null)
                {
                    notification.AddNotification("Get City", "City does not exists", NotificationModel.ENotificationType.BadRequestError);
                    return new CityResponse();
                }

                return (CityResponse)city;
            };

            return await cache.GetOrSetAsync(cacheKey, factory);
        });

        public Task<CityResponse> CreateAsync(CityRequest request) => ExecuteAsync(async () =>
        {
            Validate(request, new CityRequestValidator());

            var existingCities = await cityRepository.GetAsync(x => x.Name.Value == request.Name);
            if (existingCities.Any())
            {
                notification.AddNotification("Create City", "City already exists", NotificationModel.ENotificationType.BadRequestError);
                return new CityResponse();
            }

            var cityDomain = (Domain.CityAggregate.City)request;
            await cityRepository.InsertOrUpdateAsync(cityDomain);
            await cityRepository.SaveChangesAsync();

            await ClearCityCache(cityDomain.Id);
            return (CityResponse)cityDomain;
        });

        public Task<CityResponse> UpdateAsync(int id, CityRequest request) => ExecuteAsync(async () =>
        {
            Validate(request, new CityRequestValidator());

            var cityWithSameName = await cityRepository.GetOneNoTracking(x => x.Name.Value == request.Name);
            if (cityWithSameName is not null && cityWithSameName.Id != id)
            {
                notification.AddNotification("Update City", "City already exists", NotificationModel.ENotificationType.BadRequestError);
                return new CityResponse();
            }

            var cityFetched = await cityRepository.GetByIdAsync(id, noTracking: false);
            if (cityFetched is null)
            {
                notification.AddNotification("Update City", "City not found", NotificationModel.ENotificationType.NotFound);
                return new CityResponse();
            }

            UpdateCityProperties(cityFetched, request);
            await cityRepository.InsertOrUpdateAsync(cityFetched);
            await cityRepository.SaveChangesAsync();

            await ClearCityCache(id);
            return (CityResponse)cityFetched;
        });

        public Task<BaseResponse<object>> DeleteAsync(int id) => ExecuteAsync(async () =>
        {
            var cityFetched = await cityRepository.GetByIdAsync(id, noTracking: false);
            if (cityFetched is null)
            {
                notification.AddNotification("Delete City", "City not found", NotificationModel.ENotificationType.NotFound);
                return new BaseResponse<object>();
            }

            await cityRepository.DeleteAsync(cityFetched);
            await cityRepository.SaveChangesAsync();

            await ClearCityCache(id);
            return BaseResponse<object>.Ok(new { message = "City deleted successfully" });
        });

        private static void UpdateCityProperties(Domain.CityAggregate.City cityFetched, CityRequest request)
        {
            cityFetched.Name = new Name(request.Name);
            cityFetched.UF = new UF(request.UF);
        }

        private async Task ClearCityCache(int cityId)
        {
            await cache.RemoveAsync(
                $"{EnumCacheTags.City}:Id:{cityId}",
                $"{EnumCacheTags.City}:Page:*"
            );
        }
    }
}
