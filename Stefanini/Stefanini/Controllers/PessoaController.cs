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
            return Ok(people);
        }

        [HttpGet]
        [Route("getPersonById/{id}")]
        public async Task<ActionResult<Pessoa>> GetPersonById(int id)
        {
            var person = await _pessoaRepository.GetPessoaById(id);
            return Ok(person);
        }

        [HttpPost]
        [Route("addPerson")]
        public async Task<ActionResult> AddPerson(Pessoa pessoa)
        {
            await _pessoaRepository.AddPessoa(pessoa);
            return Ok(pessoa);
        }

        [HttpPut]
        [Route("updatePerson")]
        public async Task<ActionResult<Pessoa>> UpdatePerson(Pessoa pessoa)
        {
            await _pessoaRepository.UpdatePessoa(pessoa);
            return Ok(pessoa);
        }

        [HttpDelete]
        [Route("deletePerson")]
        public async Task<ActionResult<Pessoa>> DeletePerson(int id)
        {
            await _pessoaRepository.DeletePessoaById(id);
            return Ok(id);
        }

        [HttpGet]
        [Route("getCityByPerson/{id}")]
        public async Task<ActionResult<Pessoa>> GetCityByPerson(int id)
        {
            var city = await _pessoaRepository.GetCityByPerson(id);
            return Ok(city);
        }
    }
}
