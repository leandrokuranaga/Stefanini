using Stefanini.Domain.CityAggregate;

namespace Stefanini.Domain.PersonAggregate
{
    public class PersonDomain
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CPF { get; set; }
        public int? CityId { get; set; }
        public int Age { get; set; }
        public virtual CityDomain? City { get; set; }
    }
}
