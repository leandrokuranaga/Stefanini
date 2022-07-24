using Stefanini.Domain.Models;

namespace Stefanini.Domain.Interfaces
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Pessoa>> GetAllPessoas();
        Task<Pessoa> GetPessoaById(int id);
        Task<bool> DeletePessoaById(int id);
        Task<bool> AddPessoa(Pessoa pessoa);
        Task<bool> UpdatePessoa(Pessoa pessoa);
        Task<Cidade> GetCityByPerson(int idCity);
    }
}
