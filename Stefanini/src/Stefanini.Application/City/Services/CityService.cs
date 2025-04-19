using Microsoft.Extensions.Caching.Memory;
using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.Common;
using Stefanini.Application.Validators;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.CityAggregate.ValueObjects;
using Stefanini.Domain.SeedWork;
using Stefanini.Domain.SeedWork.Enums;
using Stefanini.Domain.SeedWork.Notification;

namespace Stefanini.Application.City.Services
{
    public class CityService(ICityRepository cityRepository, INotification notification, IMemoryCache cache) : BaseService(notification), ICityService
    {
        public Task<BasePaginatedResponse<List<CityResponse>>> GetPaginatedAsync(int page, int pageSize) =>
            ExecuteAsync<BasePaginatedResponse<List<CityResponse>>>(async () =>
            {
                var cacheKey = $"{EnumCacheTags.City}:Page:{page}:Size:{pageSize}";

                if (cache.TryGetValue(cacheKey, out BasePaginatedResponse<List<CityResponse>> cached))
                    return cached;

                var (cities, totalItems) = await cityRepository.GetPaginatedAsync(page, pageSize);

                var result = new BasePaginatedResponse<List<CityResponse>>
                {
                    Data = cities.Select(c => (CityResponse)c).ToList(),
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItens = totalItems
                };

                cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

                return result;
            });


        public Task<CityResponse> GetAsync(int id) => ExecuteAsync(async () =>
        {
            var cacheKey = $"{EnumCacheTags.City}:Id:{id}";

            if (cache.TryGetValue(cacheKey, out CityResponse cached))
                return cached;

            var city = await cityRepository.GetByIdAsync(id, false);

            if (city is null)
            {
                notification.AddNotification("Get City", "City does not exists", NotificationModel.ENotificationType.BadRequestError);
                return new CityResponse();
            }

            var cityResponse = (CityResponse)city;

            cache.Set(cacheKey, cityResponse, TimeSpan.FromMinutes(5));

            return cityResponse;
        });

        public Task<CityResponse> CreateAsync(CityRequest city) => ExecuteAsync(async () =>
        {
            Validate(city, new CityRequestValidator());

            var cityId = await cityRepository.GetAsync(x => x.Name.Value == city.Name);

            if (cityId.Count() > 0)
            {
                notification.AddNotification("Create City", "City already exists", NotificationModel.ENotificationType.BadRequestError);
                return new CityResponse();
            }

            var cityDomain = (Domain.CityAggregate.City)city;

            await cityRepository.InsertOrUpdateAsync(cityDomain);
            await cityRepository.SaveChangesAsync();

            ClearCityCache(cityDomain.Id);

            var cityResponse = (CityResponse)cityDomain;
            return cityResponse;
        });

        public Task<CityResponse> UpdateAsync(int id, CityRequest city) => ExecuteAsync(async () =>
        {
            Validate(city, new CityRequestValidator());

            var cityId = await cityRepository.GetOneNoTracking(x => x.Name.Value == city.Name);

            if (cityId is not null && cityId.Id != id)
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

            UpdateCityProperties(cityFetched, city);

            await cityRepository.InsertOrUpdateAsync(cityFetched);
            await cityRepository.SaveChangesAsync();

            ClearCityCache(id);

            var cityResponse = (CityResponse)cityFetched;
            return cityResponse;
        });

        private static void UpdateCityProperties(Domain.CityAggregate.City cityFetched, CityRequest city)
        {
            cityFetched.Name = new Name(city.Name);
            cityFetched.UF = new UF(city.UF);
        }

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

            ClearCityCache(id);

            return BaseResponse<object>.Ok(new { message = "City deleted successfully" });
        });

        private void ClearCityCache(int cityId)
        {
            cache.Remove($"{EnumCacheTags.City}:Id:{cityId}");

            for (int page = 1; page <= 10; page++)
            {
                for (int size = 5; size <= 50; size += 5)
                {
                    cache.Remove($"{EnumCacheTags.City}:Page:{page}:Size:{size}");
                }
            }
        }
    }
}
