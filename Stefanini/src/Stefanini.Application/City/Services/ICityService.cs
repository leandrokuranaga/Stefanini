using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.Common;

namespace Stefanini.Application.City.Services
{
    public interface ICityService
    {
        Task<BasePaginatedResponse<List<CityResponse>>> GetPaginatedAsync(int page, int pageSize);
        Task<CityResponse> GetAsync(int id);
        Task<CityResponse> CreateAsync(CityRequest city);
        Task<CityResponse> UpdateAsync(int id, CityRequest city);
        Task<BaseResponse<object>> DeleteAsync(int id);
    }
}
