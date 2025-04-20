using Stefanini.Domain.CityAggregate.ValueObjects;

namespace Stefanini.Application.Person.Models.Response
{
    public record PersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public int Age { get; set; }
        public string? CityName { get; set; }
        public string? UF { get; set; }

        public static explicit operator PersonResponse(Domain.CityAggregate.Entity.Person person)
        {
            return new PersonResponse
            {
                Id = person.Id,
                Name = person.Name.Value,
                Cpf = person.CPF.Value,
                Age = person.Age.Value,
                CityName = person.City?.Name?.Value,
                UF = person.City?.UF?.Value
            };
        }
    }
}
