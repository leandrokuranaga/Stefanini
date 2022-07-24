using Microsoft.AspNetCore.Mvc;
using Stefanini.Domain.Interfaces;
using Stefanini.Domain.Models;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class PessoaController : Controller
    {
        private readonly IPessoaRepository _pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        [HttpGet]
        [Route("getAllPeople")]
        public async Task<ActionResult<Pessoa>> GetAllPeople()
        {
            var people = await _pessoaRepository.GetAllPessoas();

            if (people == null)
                return NotFound();

            return Ok(people);
        }

        [HttpGet]
        [Route("getPersonById/{id}")]
        public async Task<ActionResult<Pessoa>> GetPersonById(int id)
        {
            var person = await _pessoaRepository.GetPessoaById(id);

            if(person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [Route("addPerson")]
        public async Task<ActionResult> AddPerson(Pessoa pessoa)
        {
            var person = await _pessoaRepository.AddPessoa(pessoa);

            if (!person)
                return StatusCode(400);

            return Ok(pessoa);
        }

        [HttpPut]
        [Route("updatePerson")]
        public async Task<ActionResult<Pessoa>> UpdatePerson(Pessoa pessoa)
        {
            var updated = await _pessoaRepository.UpdatePessoa(pessoa);

            if (!updated)
                return NotFound();

            return Ok(pessoa);
        }

        [HttpDelete]
        [Route("deletePerson")]
        public async Task<ActionResult<Pessoa>> DeletePerson(int id)
        {
            var deleted = await _pessoaRepository.DeletePessoaById(id);

            if(deleted == false)
                return NotFound();

            return Ok(true);
        }

        [HttpGet]
        [Route("getCityByPerson/{id}")]
        public async Task<ActionResult<Pessoa>> GetCityByPerson(int id)
        {
            var city = await _pessoaRepository.GetCityByPerson(id);

            if (city == null)
                return NotFound();

            return Ok(city);
        }
    }
}
