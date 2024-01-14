using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Domain.CityAggregate;

namespace Stefanini.Application.City.Services
{
    public interface ICityService
    {
        Task<List<CityResponse>> GetAllCities();
        Task<CityResponse> GetCityById(int id);
        Task AddCity(CityRequest city);
    }
}
