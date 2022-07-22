using Microsoft.AspNetCore.Mvc;
using Stefanini.Domain.Interfaces;
using Stefanini.Domain.Models;

namespace Stefanini.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    public class CidadeController : Controller
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeController(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }

        [HttpGet]
        [Route("getAllCities")]
        public async Task<ActionResult<Cidade>> GetAllCities()
        {
            var cities = await _cidadeRepository.GetAllCidades();
            return Ok(cities);
        }

        [HttpGet]
        [Route("getCityById/{id}")]
        public async Task<ActionResult<Cidade>> GetCityById(int id)
        {
            var city = await _cidadeRepository.GetCidadeById(id);
            return Ok(city);
        }

        [HttpPost]
        [Route("addCity")]
        public async Task<ActionResult> AddCity(Cidade cidade)
        {
            await _cidadeRepository.AddCidade(cidade);
            return Ok(cidade);
        }

    }
}
