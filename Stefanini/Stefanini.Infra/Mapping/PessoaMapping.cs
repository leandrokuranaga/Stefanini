using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stefanini.Domain.Models;

namespace Stefanini.Infra.Mapping
{
    internal class PessoaMapping : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(c => c.CPF)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.Property(c => c.Idade)
                .IsRequired()
                .HasColumnType("integer");

            builder.HasOne(c => c.cidade)
                    .WithOne(z => z.pessoa);

            builder.ToTable("Pessoa");
        }
    }
}
