using Stefanini.Application.Person.Models.Request;
using Swashbuckle.AspNetCore.Filters;

namespace Stefanini.API.SwaggerExamples.Person
{
    public class PersonRequestExample : IExamplesProvider<PersonRequest>
    {
        public PersonRequest GetExamples()
        {
            return new PersonRequest
            {
                Name = "Leandro Kuranaga",
                CPF = "45845678901",
                CityId = 1,
                Age = 30
            };
        }
    }
}
