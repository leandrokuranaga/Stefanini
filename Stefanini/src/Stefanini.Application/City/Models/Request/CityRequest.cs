using Stefanini.Domain.CityAggregate;
using System.ComponentModel.DataAnnotations;

namespace Stefanini.Application.City.Models.Request;

public record CityRequest
{
    public string? Name { get; set; }
    public string? UF { get; set; }

    public static explicit operator CityDomain(CityRequest domain)
    {
        return new CityDomain
        {
            Name = domain.Name,
            UF = domain.UF
        }; 
            
    }
}
