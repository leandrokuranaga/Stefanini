using Stefanini.Domain.CityAggregate.Entity;
using Stefanini.Domain.CityAggregate.ValueObjects;

namespace Stefanini.Domain.CityAggregate
{
    public class City : Abp.Domain.Entities.Entity
    {
        public Name Name { get; set; }
        public UF UF { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
