using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.Interfaces;
using Stefanini.Domain.Models;
using Stefanini.Infra.Context;

namespace Stefanini.Infra.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DatabaseContext _context;

        public PessoaRepository(DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> AddPessoa(Pessoa pessoa)
        {

            var person = await _context
                                .Pessoa
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.CPF == pessoa.CPF);

            if (person != null)
                return false;

            var addedPerson = _context.Pessoa.AddAsync(pessoa);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeletePessoaById(int id)
        {

            var pessoa = await _context
                                .Pessoa
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (pessoa == null)
                return false;

            var x = _context.Remove(new Pessoa { Id = id });
            await _context.SaveChangesAsync();

            return true;
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

        public async Task<bool> UpdatePessoa(Pessoa pessoa)
        {
            var updated = await _context
                                .Pessoa
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == pessoa.Id);

            if (updated == null)
                return false;

            _context.Pessoa.Update(pessoa);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
