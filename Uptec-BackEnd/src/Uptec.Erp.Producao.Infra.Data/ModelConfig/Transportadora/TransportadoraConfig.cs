using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;
using TransportadoraAlias = Uptec.Erp.Producao.Domain.Transportadoras.Models.Transportadora;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Transportadora
{
    public class TransportadoraConfig : IEntityTypeConfiguration<TransportadoraAlias>
    {
        public void Configure(EntityTypeBuilder<TransportadoraAlias> builder)
        {
            builder.HasKey(t => t.Id);

            builder.OwnsOne(t => t.Cnpj)
                .Ignore(t => t.EhValido)
                .Property(t => t.Numero)
                    .HasColumnName("Cnpj")
                    .HasColumnType($"varchar({Cnpj.MaxLength})")
                    .IsRequired();

            builder.Property(t => t.InscricaoEstadual)
                .HasColumnType($"varchar({TransportadoraAlias.InscricaoEstadualMaxLength})");

            builder.Property(t => t.RazaoSocial)
                .HasColumnType($"varchar({TransportadoraAlias.RazaoSocialMaxLength})")
                .IsRequired();

            builder.Property(t => t.NomeFantasia)
                .HasColumnType($"varchar({TransportadoraAlias.NomeFantasiaMaxLength})")
                .IsRequired();

            builder.OwnsOne(t => t.Email)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.EnderecoEmail)
                .HasColumnType($"varchar({Email.MaxLength})")
                .HasColumnName("Email");

            builder.Property(t => t.Website)
                .HasColumnType($"varchar({TransportadoraAlias.WebsiteMaxLength})");

            builder.Property(t => t.Observacoes)
                .HasColumnType($"varchar({TransportadoraAlias.ObservacoesMaxLength})");

            builder.Ignore(t => t.Validation);
            builder.Ignore(t => t.Endereco);
            builder.Ignore(t => t.Telefone);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("Transportadoras");
        }
    }
}