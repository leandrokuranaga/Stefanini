using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Person.Services;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class PersonController(
        IPersonService personService,
        ILogger<PersonController> logger
            ) : Controller
    {
        private readonly IPersonService _personService = personService;
        private readonly ILogger<PersonController> _logger = logger;

        [HttpGet]
        public async Task<ActionResult<PersonResponse>> GetAllPeople()
        {
            var people = await _personService.GetAllPeople();

            if (people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PersonResponse>> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);

            if (person is null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult> AddPerson(PersonRequest person)
        {            
            await _personService.AddPerson(person);
            return Ok(); 
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<PersonResponse>> UpdatePerson(PersonRequest person, int id)
        {
            await _personService.UpdatePerson(person, id);

            return Ok(person);          
        }

        [HttpDelete]
        public async Task<ActionResult<PersonResponse>> DeletePerson(int id)
        {
            await _personService.DeletePerson(id);

            return Ok();
        }

        [HttpPut]
        [Route("{personId}/city/{cityId}")]
        public async Task<ActionResult> AddPersonToCity(int personId, int cityId)
        {
            var added = await _personService.AddPersonToCity(personId, cityId);

            if (!added)
                return NotFound();

            return Ok();
        }
    }
}
