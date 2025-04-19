using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.City.Models.Request;
using Stefanini.Application.City.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using Stefanini.Application.Common;
using Stefanini.Application.City.Models.Response;
using Stefanini.Api;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Domain.CityAggregate.Entity;
using Stefanini.Domain.SeedWork.Notification;

namespace Stefanini.Controllers
{
    /// <summary>
    /// City controller destinated for manage cities.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CityController(ICityService cityService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Gets all cities.
        /// </summary>
        /// <param name="page">Number of the page you want to show about the city data.</param>
        /// <param name="size">Number of items you want to show.</param>
        /// <returns>A list of cities paginated</returns>
        [HttpGet]
        [SwaggerOperation("Get all cities")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllCities([FromQuery] int page = 1, int size = 10)
        {
            var cities = await cityService.GetPaginatedAsync(page, size);
            return Response(BaseResponse<BasePaginatedResponse<List<CityResponse>>>.Ok(cities));
        }

        /// <summary>
        /// Gets a specific city by its id
        /// </summary>
        /// <param name="id">The ID of the city to be updated (must be greater than 0).</param>
        /// <returns>A city based on id</returns>
        [HttpGet("{id:int:min(1)}")]
        [SwaggerOperation("Get city")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetCityById(int id)
        {
            var city = await cityService.GetAsync(id);
            return Response(BaseResponse<CityResponse>.Ok(city));
        }

        /// <summary>
        /// Create a new city.
        /// </summary>
        /// <param name="request">The city object data.</param>
        /// <returns>A created city</returns>
        [HttpPost]
        [SwaggerOperation("Create city")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddCity(CityRequest city)
        {
            var result = await cityService.CreateAsync(city);
            return Response(BaseResponse<CityResponse>.Ok(result));
        }

        /// <summary>
        /// Update a city.
        /// </summary>
        /// <param name="id">The ID of the city to be updated (must be greater than 0).</param>
        /// <param name="request">The city object data to be updated.</param>
        /// <returns>An updated city</returns>
        [HttpPatch("{id:int:min(1)}")]
        [SwaggerOperation("Update a city")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateAsync(int id, CityRequest city)
        {
            var result = await cityService.UpdateAsync(id, city);
            return Response(BaseResponse<CityResponse>.Ok(result));
        }

        /// <summary>
        /// Delete a city.
        /// </summary>
        /// <param name="id">The id of the city to be deleted.</param>
        /// <returns>A deleted city</returns>
        [HttpDelete]
        [SwaggerOperation("Delete a city")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await cityService.DeleteAsync(id);
            return Response(BaseResponse<EmptyResultModel>.Ok(new EmptyResultModel()));
        }

    }
}
