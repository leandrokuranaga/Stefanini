using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stefanini.Domain.PersonAggregate;

namespace Stefanini.Infra.Mapping
{
    internal class PersonMapping : IEntityTypeConfiguration<PersonDomain>
    {
        public void Configure(EntityTypeBuilder<PersonDomain> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.Age)
                .IsRequired()
                .HasColumnType("integer");

            builder.HasOne(c => c.City)
                    .WithOne(z => z.Person);

            builder.ToTable("Person");
        }
    }
}
