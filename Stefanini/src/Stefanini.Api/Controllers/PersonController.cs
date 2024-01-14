using Microsoft.AspNetCore.Mvc;
using Stefanini.Application.Person.Models.Request;
using Stefanini.Application.Person.Models.Response;
using Stefanini.Application.Person.Services;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(
            IPersonService personService,
            ILogger<PersonController> logger
            )
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        [Route("all-people")]
        public async Task<ActionResult<PersonResponse>> GetAllPeople()
        {
            var people = await _personService.GetAllPeople();

            if (people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpGet]
        [Route("person-by-id/{id}")]
        public async Task<ActionResult<PersonResponse>> GetPersonById(int id)
        {
            var person = await _personService.GetPersonById(id);

            if(person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [Route("person")]
        public async Task<ActionResult> AddPerson(PersonRequest person)
        {
            try
            {
                await _personService.AddPerson(person);
                return Ok(); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpPut]
        [Route("person/{id}")]
        public async Task<ActionResult<PersonResponse>> UpdatePerson(PersonRequest person, int id)
        {
            try
            {
                var updated = await _personService.UpdatePerson(person, id);

                return Ok(person);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal Server Error");
            }

        }

        [HttpDelete]
        [Route("person")]
        public async Task<ActionResult<PersonResponse>> DeletePerson(int id)
        {
            var deleted = await _personService.DeletePerson(id);

            return Ok(true);
        }
    }
}
