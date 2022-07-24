using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.Interfaces;
using Stefanini.Domain.Models;
using Stefanini.Infra.Context;

namespace Stefanini.Infra.Repository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly DatabaseContext _context;

        public CidadeRepository(DatabaseContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> AddCidade(Cidade cidade)
        {
            _context.Cidade.AddAsync(cidade);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Cidade>> GetAllCidades()
        {

            var cities = await _context
                                .Cidade
                                .AsNoTracking()
                                .ToListAsync();

            return cities;
        }

        public async Task<Cidade> GetCidadeById(int id)
        {
            var city = await _context
                                .Cidade
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);

            return city;
        }

    }
}
