using Stefanini.Domain.Models;

namespace Stefanini.Domain.Interfaces
{
    public interface ICidadeRepository
    {
        Task<IEnumerable<Cidade>> GetAllCidades();
        Task<Cidade> GetCidadeById(int id);
        Task AddCidade(Cidade cidade);
    }
}
