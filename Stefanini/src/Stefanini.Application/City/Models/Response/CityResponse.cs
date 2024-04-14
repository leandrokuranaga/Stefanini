using Stefanini.Domain.CityAggregate;

namespace Stefanini.Application.City.Models.Response
{
    public record CityResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? UF { get; set; }

        public static explicit operator CityResponse(CityDomain city)
        {
            return new CityResponse
            {
                Id = city.Id,
                Name = city.Name,
                UF = city.UF
            };
        }
    }
}
