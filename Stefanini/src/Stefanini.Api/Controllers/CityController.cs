using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.City.Services;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CityController(ICityService cityService) : Controller
    {
        private readonly ICityService _cityService = cityService;

        [HttpGet]
        public async Task<ActionResult<CityResponse>> GetAllCities()
        {
            var cities = await _cityService.GetAllCities();

            if (cities == null)
                return NotFound();

            return Ok(cities);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CityResponse>> GetCityById(int id)
        {
            var city = await _cityService.GetCityById(id);
            
            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> AddCity(CityRequest city)
        {
            try
            {
                await _cityService.AddCity(city);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
