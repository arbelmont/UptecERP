using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;
using FornecedorAlias = Uptec.Erp.Producao.Domain.Fornecedores.Models.Fornecedor;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fornecedores
{
    public class FornecedorConfig : IEntityTypeConfiguration<FornecedorAlias>
    {
        public void Configure(EntityTypeBuilder<FornecedorAlias> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(c => c.Cnpj)
                .Ignore(c => c.EhValido)
                .Property(c => c.Numero)
                    .HasColumnName("Cnpj")
                    .HasColumnType($"varchar({Cnpj.MaxLength})")
                    .IsRequired();

            builder.Property(c => c.InscricaoEstadual)
                .HasColumnType($"varchar({FornecedorAlias.InscricaoEstadualMaxLength})");

            builder.Property(c => c.RazaoSocial)
                .HasColumnType($"varchar({FornecedorAlias.RazaoSocialMaxLength})")
                .IsRequired();

            builder.Property(c => c.NomeFantasia)
                .HasColumnType($"varchar({FornecedorAlias.NomeFantasiaMaxLength})")
                .IsRequired();

            builder.OwnsOne(c => c.Email)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.EnderecoEmail)
                .HasColumnType($"varchar({Email.MaxLength})")
                .HasColumnName("Email");

            builder.Property(c => c.Website)
                .HasColumnType($"varchar({FornecedorAlias.WebsiteMaxLength})");

            builder.Property(c => c.Observacoes)
                .HasColumnType($"varchar({FornecedorAlias.ObservacoesMaxLength})");

            builder.Ignore(c => c.Validation);
            builder.Ignore(t => t.Endereco);
            builder.Ignore(t => t.Telefone);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("Fornecedores");
        }
    }
}

