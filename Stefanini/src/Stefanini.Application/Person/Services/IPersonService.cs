using Stefanini.Application.Common;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;

namespace Stefanini.Application.Person.Services
{
    public interface IPersonService
    {
        Task<BasePaginatedResponse<List<PersonResponse>>> GetPaginatedAsync(int page, int pageSize);
        Task<PersonResponse> GetAsync(int id);
        Task<PersonResponse> CreateAsync(PersonRequest person);
        Task<PersonResponse> UpdateAsync(int id, PersonRequest person);
        Task<BaseResponse<object>> DeleteAsync(int id);
    }
}
