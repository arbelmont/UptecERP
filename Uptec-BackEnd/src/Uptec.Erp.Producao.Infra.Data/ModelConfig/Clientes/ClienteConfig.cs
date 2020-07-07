using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;
using ClienteAlias = Uptec.Erp.Producao.Domain.Clientes.Models.Cliente;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Clientes
{
    public class ClienteConfig : IEntityTypeConfiguration<ClienteAlias>
    {
        public void Configure(EntityTypeBuilder<ClienteAlias> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Cnpj)
                .Ignore(c => c.EhValido)
                .Property(c => c.Numero)
                    .HasColumnName("Cnpj")
                    .HasColumnType($"varchar({Cnpj.MaxLength})")
                    .IsRequired();

            builder.Property(c => c.InscricaoEstadual)
                .HasColumnType($"varchar({ClienteAlias.InscricaoEstadualMaxLength})");

            builder.Property(c => c.RazaoSocial)
                .HasColumnType($"varchar({ClienteAlias.RazaoSocialMaxLength})")
                .IsRequired();

            builder.Property(c => c.NomeFantasia)
                .HasColumnType($"varchar({ClienteAlias.NomeFantasiaMaxLength})")
                .IsRequired();

            builder.OwnsOne(c => c.Email)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.EnderecoEmail)
                .HasColumnType($"varchar({Email.MaxLength})")
                .HasColumnName("Email");

            builder.Property(c => c.Website)
                .HasColumnType($"varchar({ClienteAlias.WebsiteMaxLength})");

            builder.Property(c => c.Observacoes)
                .HasColumnType($"varchar({ClienteAlias.ObservacoesMaxLength})");

            builder.Ignore(c => c.Validation);
            builder.Ignore(t => t.Endereco);
            builder.Ignore(t => t.Telefone);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasOne(c => c.Transportadora)
                .WithMany(t => t.Clientes)
                .HasForeignKey(c => c.TransportadoraId);

            builder.ToTable("Clientes");
        }
    }
}

