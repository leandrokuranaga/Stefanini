using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.Common;
using Stefanini.Application.Validators;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.SeedWork.Notification;

namespace Stefanini.Application.City.Services
{
    public class CityService(ICityRepository cityRepository, INotification notification) : BaseService(notification), ICityService
    {
        private readonly ICityRepository _cityRepository = cityRepository;
        private readonly INotification _notification = notification;

        public async Task AddCity(CityRequest city)
        {
            try
            {
                Validate(city, new CityRequestValidator());

                var cityDomain = (CityDomain)city;

                await _cityRepository.AddCity(cityDomain);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<List<CityResponse>> GetAllCities()
        {
            var cities = await _cityRepository.GetAllAsync();
            
            var cityResponse = cities.Select(city => (CityResponse)city).ToList();

            return cityResponse;
        }

        public async Task<CityResponse> GetCityById(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id, false);

            var cityResponse = (CityResponse)city;

            return cityResponse;
        }
    }
}
