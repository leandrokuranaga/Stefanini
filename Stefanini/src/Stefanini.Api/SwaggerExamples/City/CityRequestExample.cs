using Stefanini.Application.City.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Stefanini.API.SwaggerExamples.City
{
    public class CityRequestExample : IExamplesProvider<CityRequest>
    {
        public CityRequest GetExamples()
        {
            return new CityRequest
            {
                Name = "Indaiatuba",
                UF = "SP"
            };
        }
    }
}
