using System.ComponentModel.DataAnnotations;

namespace Stefanini.Application.City.Models.Request
{
    public class CityRequest
    {
        public string? Name { get; set; }
        [StringLength(2, ErrorMessage = "UF must be 2 characters long.")]
        public string? UF { get; set; }
    }
}
