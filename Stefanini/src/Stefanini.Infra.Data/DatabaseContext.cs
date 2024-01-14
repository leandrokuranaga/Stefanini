using Microsoft.EntityFrameworkCore;
using Stefanini.Domain.CityAggregate;
using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Infra.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<PersonDomain> Person { get; set; }
        public DbSet<CityDomain> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Entity<CityDomain>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            modelBuilder.Entity<CityDomain>()
                .HasOne(b => b.Person)
                .WithOne(i => i.City)
                .HasForeignKey<PersonDomain>(i => i.CityId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                                            .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }

    }
}
