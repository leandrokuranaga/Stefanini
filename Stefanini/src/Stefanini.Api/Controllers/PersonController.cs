using Microsoft.AspNetCore.Mvc;
using Stefanini.Api;
using Stefanini.Application.Common;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Person.Services;
using Stefanini.Domain.SeedWork.Notification;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Stefanini.Controllers
{
    /// <summary>
    /// Person controller destinated to register and manage people.
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class PersonController(IPersonService personService, INotification notification) : BaseController(notification)
    {
        /// <summary>
        /// Returns a list of people paginated
        /// </summary>
        /// <param name="page">Number of the page you want to show about the person data.</param>
        /// <param name="size">Number of items you want to show.</param>
        /// <returns>A list of people paginated</returns>
        [HttpGet]
        [SwaggerOperation("Get all people")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAllPeople([FromQuery]int page = 1, int size = 10)
        {
            var people = await personService.GetPaginatedAsync(page, size);
            return Response(BaseResponse<BasePaginatedResponse<List<PersonResponse>>>.Ok(people));
        }

        /// <summary>
        /// Gets a person by its id
        /// </summary>
        /// <param name="id">ID of the person to be fetched (must be greater than 0).</param>
        /// <returns>A person based on id</returns>
        [HttpGet("{id:int:min(1)}")]
        [SwaggerOperation("Get person by its id")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await personService.GetAsync(id);
            return Response(BaseResponse<PersonResponse>.Ok(person));
        }

        /// <summary>
        /// Create a new person
        /// </summary>
        /// <param name="person">Object to add a new person it can be added to a city or not</param>
        /// <returns>Created person</returns>
        [HttpPost]
        [SwaggerOperation("Create Person")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddPerson(PersonRequest person)
        {            
            var result = await personService.CreateAsync(person);
            return Response(BaseResponse<PersonResponse>.Ok(result)); 
        }

        /// <summary>
        /// Updates a person
        /// </summary>
        /// <param name="id">ID of the person to be updated (must be greater than 0).</param>
        /// <param name="person">The updated person data.</param>
        /// <returns>Updated person</returns>
        [HttpPatch("{id:int:min(1)}")]
        [SwaggerOperation("Update Person")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdatePerson(int id, PersonRequest person)
        {
            var result = await personService.UpdateAsync(id, person);
            return Response(BaseResponse<PersonResponse>.Ok(result));
        }

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id">The ID of the promotion to be updated (must be greater than 0).</param>
        /// <returns>Deleted person</returns>
        [HttpDelete("{id:int:min(1)}")]
        [SwaggerOperation("Delete person")]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IActionResult), (int)HttpStatusCode.BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeletePerson(int id)
        {
            await personService.DeleteAsync(id);
            return Response(BaseResponse<EmptyResultModel>.Ok(new EmptyResultModel()));
        }
    }
}
