
using Stefanini.Domain.CityAggregate.ValueObjects;

namespace Stefanini.Application.Person.Models.Request
{
    public record PersonRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public int Age { get; set; }
        public int? CityId { get; set; }

        public static explicit operator Domain.CityAggregate.Entity.Person(PersonRequest person)
        {
            return new Domain.CityAggregate.Entity.Person
            {
                Name = new Name(person.Name),
                CPF = new CPF(person.CPF),
                Age = new Age(person.Age),
                CityId = person.CityId
            };
        }
    }
}
