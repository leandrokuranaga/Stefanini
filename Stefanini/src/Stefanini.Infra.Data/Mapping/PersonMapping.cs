using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stefanini.Domain.CityAggregate.Entity;
using Stefanini.Infra.Data.Mapping.Seed;

namespace Stefanini.Infra.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(p => p.Id);

            builder.OwnsOne(p => p.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();

                name.WithOwner();
                name.HasData(PersonSeed.PersonNames());
            });

            builder.OwnsOne(p => p.CPF, cpf =>
            {
                cpf.Property(c => c.Value)
                   .HasColumnName("CPF")
                   .HasMaxLength(11)
                   .IsRequired();

                cpf.WithOwner();

                cpf.HasIndex(p => p.Value)
                    .HasDatabaseName("IX_Person_CPF")
                    .IsUnique();

                cpf.HasData(PersonSeed.PersonCPFs());

            });

            builder.OwnsOne(p => p.Age, age =>
            {
                age.Property(a => a.Value)
                   .HasColumnName("Age")
                   .IsRequired();

                age.WithOwner();
                age.HasData(PersonSeed.PersonAges());
            });

            builder.HasOne(c => c.City)
                .WithMany(c => c.Person)
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.SetNull);         

            builder.HasIndex(c => c.CityId)
                .HasDatabaseName("IX_Person_CityId");

            builder.HasData(PersonSeed.People());
        }
    }
}
