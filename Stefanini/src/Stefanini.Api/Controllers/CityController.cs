using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Models.Response;
using Stefanini.Application.City.Services;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [Route("all-cities")]
        public async Task<ActionResult<CityResponse>> GetAllCities()
        {
            var cities = await _cityService.GetAllCities();

            if (cities == null)
                return NotFound();

            return Ok(cities);
        }

        [HttpGet]
        [Route("city-by-id/{id}")]
        public async Task<ActionResult<CityResponse>> GetCityById(int id)
        {
            var city = await _cityService.GetCityById(id);
            
            if (city == null)
                return NotFound();

            return Ok(city);
        }

        [HttpPost]
        [Route("city")]
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
