using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal
{
    public class NotaEntradaConfig : IEntityTypeConfiguration<NotaEntrada>
    {
        public void Configure(EntityTypeBuilder<NotaEntrada> builder)
        {
            builder.HasKey(nfe => nfe.Id);

            builder.Property(nfe => nfe.Data)
                .IsRequired();

            builder.Property(nfe => nfe.NumeroNota)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntrada.NumeroNotaMaxLenght})");

            builder.Property(nfe => nfe.Valor)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.Cfop)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntrada.CfopMaxLenght})");

            builder.OwnsOne(nfe => nfe.CnpjEmissor)
               .Ignore(c => c.EhValido)
               .Property(c => c.Numero)
                   .HasColumnName("CnpjEmissor")
                   .HasColumnType($"varchar({Cnpj.MaxLength})")
                   .IsRequired();

            builder.Property(nfe => nfe.NomeEmissor)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntrada.NomeEmissorMaxLenght})");

            builder.OwnsOne(nfe => nfe.EmailEmissor)
                .Ignore(nfe => nfe.CascadeMode)
                .Property(nfe => nfe.EnderecoEmail)
                .HasColumnType($"varchar({Email.MaxLength})")
                .HasColumnName("EmailEmissor");

            builder.Ignore(nfe => nfe.Validation);
            builder.Ignore(nfe => nfe.Inconsistencias);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasIndex(nfe => nfe.NumeroNota).IsUnique();

            builder.ToTable("NotasEntrada");
        }
    }
}
