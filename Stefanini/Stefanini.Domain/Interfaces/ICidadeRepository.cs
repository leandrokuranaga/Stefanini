using Stefanini.Domain.Models;

namespace Stefanini.Domain.Interfaces
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<Cidade>> GetAllCidades();
        Task<Cidade> GetCidadeById(int id);
        Task<bool> AddCidade(Cidade cidade);
    }
}
