using Stefanini.Domain.CityAggregate.ValueObjects;

namespace Stefanini.Application.City.Models.Request;

public record CityRequest
{
    public string? Name { get; set; }
    public string? UF { get; set; }

    public static explicit operator Domain.CityAggregate.City(CityRequest domain)
    {
        return new Domain.CityAggregate.City
        {
            Name = new Name(domain.Name),
            UF = new UF(domain.UF)
        };             
    }
}
