using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Passageiro.Domain.Entities;

namespace Passageiro.Repository.Mappings {
    public class PassageiroMap : IEntityTypeConfiguration<PassageiroDomain> {
        public void Configure (EntityTypeBuilder<PassageiroDomain> builder) {
            builder.Property (t => t.Id).IsRequired ().HasColumnType ("int");
            builder.Property (t => t.Nome).HasColumnType ("varchar(50)");
            builder.Property (t => t.Idade).HasColumnType ("int");
            builder.Property (t => t.Celular).HasColumnType ("varchar(20)");
            builder.Property (t => t.DataCriacao).HasColumnType ("datetime");

            builder.ToTable ("Passageiro");
        }
    }
}