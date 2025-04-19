using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stefanini.Domain.CityAggregate;
using Stefanini.Infra.Data.Mapping.Seed;

namespace Stefanini.Infra.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(c => c.Name, n =>
            {
                n.Property(p => p.Value)
                 .HasColumnName("Name")
                 .HasMaxLength(100)
                 .IsRequired();

                n.WithOwner().HasForeignKey("CityId");

                n.HasIndex(p => p.Value).HasDatabaseName("IX_City_Name").IsUnique();

                n.HasData(CitySeed.CityNames());
            });


            builder.OwnsOne(c => c.UF, uf =>
            {
                uf.Property(p => p.Value)
                  .HasColumnName("UF")
                  .HasMaxLength(2)
                  .IsRequired();

                uf.WithOwner();
                uf.HasData(CitySeed.CityUFs());
            });

            builder.HasData(CitySeed.Cities());
        }

    }
}
