using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.Interfaces;
using Stefanini.Domain.Models;
using Stefanini.Infra.Context;

namespace Stefanini.Infra.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DatabaseContext _context;

        public PessoaRepository (DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task AddPessoa(Pessoa pessoa)
        {
             _context.Pessoa.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePessoaById(int id)
        {
            _context.Remove(new Pessoa { Id = id });
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pessoa>> GetAllPessoas()
        {
            var pessoas = await _context
                                .Pessoa
                                .AsNoTracking()
                                .ToListAsync();

            return pessoas;
        } 

        public async Task<Pessoa> GetPessoaById(int id)
        {
            var pessoa = await _context
                                .Pessoa
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            return pessoa;
        }

        public async Task<Cidade> GetCityByPerson(int idCity)
        {
            var cidade = await _context
                                .Cidade
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == idCity);

            return cidade;
        }

        public async Task UpdatePessoa(Pessoa pessoa)
        {
            _context.Pessoa.Update(pessoa);
            await _context.SaveChangesAsync();
        }

    }
}
