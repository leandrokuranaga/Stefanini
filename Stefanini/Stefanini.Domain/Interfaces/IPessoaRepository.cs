using Stefanini.Domain.Models;

namespace Stefanini.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllPessoas();
        Task<Pessoa> GetPessoaById(int id);
        Task DeletePessoaById(int id);
        Task AddPessoa(Pessoa pessoa);
        Task UpdatePessoa(Pessoa pessoa);
        Task<Cidade> GetCityByPerson(int idCity);
    }
}
