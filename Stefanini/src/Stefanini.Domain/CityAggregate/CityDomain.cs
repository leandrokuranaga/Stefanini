using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Domain.CityAggregate
{
    public class CityDomain
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UF { get; set; }
        public virtual PersonDomain? Person { get; set; }
    }
}
