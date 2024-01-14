using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Domain.CityAggregate;

namespace Stefanini.Application.City.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task AddCity(CityRequest city)
        {
            try
            {
                CityDomain cityDomain = new()
                {
                    Name = city.Name,
                    UF = city.UF
                };

                await _cityRepository.AddCity(cityDomain);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        public async Task<List<CityResponse>> GetAllCities()
        {
            var cities = await _cityRepository.GetAllAsync();
            
            List<CityResponse> cityResponse = [];

            foreach (var city in cities)
            {
                CityResponse cityResult = new()
                {
                    Id = city.Id,
                    Name = city.Name,
                    UF = city.UF
                };
                cityResponse.Add(cityResult);
            }
            return cityResponse;
        }

        public async Task<CityResponse> GetCityById(int id)
        {
            var city = await _cityRepository.GetByIdAsync(id, false);

            CityResponse cityResponse = new()
            {
                Id = city.Id,
                Name = city.Name,
                UF = city.UF
            };

            return cityResponse;
        }
    }
}
