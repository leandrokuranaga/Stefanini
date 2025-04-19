using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.CityAggregate.Entity;

namespace Stefanini.Infra.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }

    }
}
