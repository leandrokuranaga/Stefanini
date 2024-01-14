using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stefanini.Domain.CityAggregate;

namespace Stefanini.Infra.Mapping
{
    public class CityMapping : IEntityTypeConfiguration<CityDomain>
    {
        public void Configure(EntityTypeBuilder<CityDomain> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.UF)
                .IsRequired()
                .HasColumnType("varchar(2)");

            builder.ToTable("City");
        }
    }
}
