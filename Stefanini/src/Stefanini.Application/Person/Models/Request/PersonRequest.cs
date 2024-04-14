using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Application.Person.Models.Request
{
    public class PersonRequest
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public int Age { get; set; }

        public static explicit operator PersonDomain(PersonRequest person)
        {
            return new PersonDomain
            {
                Name = person.Name,
                CPF = person.CPF,
                Age = person.Age
            };
        }
    }
}
