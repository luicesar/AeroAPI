using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Passageiro.Domain.Entities;

namespace Passageiro.Repository.Mappings {
    public class ControleAcessoMap : IEntityTypeConfiguration<ControleAcessoDomain> {
        public void Configure (EntityTypeBuilder<ControleAcessoDomain> builder) {
            builder.Property (t => t.Id).IsRequired ().HasColumnType ("int");
            builder.Property (t => t.Login).HasColumnType ("varchar(50)");
            builder.Property (t => t.Senha).HasColumnType ("varchar(50)");
            builder.Property (t => t.TipoUsuario).HasColumnType ("int");
            builder.Property (t => t.DataCriacao).HasColumnType ("datetime");

            builder.ToTable ("ControleAcesso");
        }
    }
}