using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco;
using Uptec.Erp.Shared.Domain.ValueObjects;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fornecedor
{
    public class FornecedorEnderecoConfig : IEntityTypeConfiguration<FornecedorEndereco>
    {

        public void Configure(EntityTypeBuilder<FornecedorEndereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Logradouro)
                .HasColumnName("Logradouro")
                .HasColumnType($"varchar({Endereco.LogradouroMaxLength})");

            builder.Property(e => e.Numero)
                .HasColumnName("Numero")
                .HasColumnType($"varchar({Endereco.NumeroMaxLength})");

            builder.Property(e => e.Bairro)
                .HasColumnName("Bairro")
                .HasColumnType($"varchar({Endereco.BairroMaxLength})");

            builder.Property(e => e.Cep)
                .HasColumnName("Cep")
                .HasColumnType($"varchar({Endereco.CepMaxLength})");

            builder.Property(e => e.Complemento)
                .HasColumnName("Complemento")
                .HasColumnType($"varchar({Endereco.ComplementoMaxLength})");

            builder.OwnsOne(e => e.Cidade)
                .Ignore(e => e.Codigo)
                .Ignore(e => e.Uf)
                .Property(e => e.Nome)
                    .HasColumnName("Cidade")
                    .HasColumnType($"varchar({Cidade.NomeCidadeMaxLength})");

            builder.OwnsOne(e => e.Estado)
                .Ignore(e => e.Codigo)
                .Ignore(e => e.NomeEstado)
                .Ignore(e => e.Regiao)
                .Ignore(e => e.AliquotaIcms)
                .Ignore(e => e.AliquotaBaseCalculo)
                .Property(e => e.Sigla)
                    .HasColumnName("Estado")
                    .HasColumnType($"varchar({Estado.SiglaMaxLength})");

            builder.Ignore(e => e.Validation);
            builder.Ignore(e => e.Obrigatorio);

            builder.HasOne(e => e.Fornecedor)
                   .WithMany(e => e.Enderecos)
                   .IsRequired();

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("FornecedorEnderecos");
        }
    }
}
