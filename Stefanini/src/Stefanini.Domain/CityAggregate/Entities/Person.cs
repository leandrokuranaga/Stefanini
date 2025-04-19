
using Stefanini.Domain.CityAggregate.ValueObjects;

namespace Stefanini.Domain.CityAggregate.Entity
{
    public class Person : Abp.Domain.Entities.Entity
    {
        public Name Name { get; set; }
        public CPF CPF { get; set; }
        public int? CityId { get; set; }
        public Age Age { get; set; }
        public virtual City? City { get; set; }
    }
}
