using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal
{
    public class NotaSaidaConfig : IEntityTypeConfiguration<NotaSaida>
    {
        public void Configure(EntityTypeBuilder<NotaSaida> builder)
        {
            builder.HasKey(nfe => nfe.Id);

            builder.Property(nfe => nfe.Data)
                .IsRequired();

            builder.Property(nfe => nfe.NumeroNota)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaida.NumeroNotaMaxLenght})");

            builder.Property(nfe => nfe.NaturezaOperacao)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaida.NaturezaOperacaoMaxLenght})");

            builder.Property(nfe => nfe.Data)
                .IsRequired();

            builder.Property(nfe => nfe.AliquotaIpi)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorBaseCalculo)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorIcms)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorTotalProdutos)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorFrete)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorSeguro)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorDesconto)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorOutrasDespesas)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorIpi)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorPis)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorCofins)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.ValorTotalNota)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(nfe => nfe.OutrasInformacoes)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaida.OutrasInformacoesMaxLenght})");

            builder.Property(nfe => nfe.ErroApi)
                .HasColumnType($"varchar({NotaSaida.ErroApiMaxLenght})");


            builder.Ignore(nfe => nfe.Validation);
            builder.Ignore(nfe => nfe.EnderecoDestinatario);
            builder.Ignore(nfe => nfe.EnderecoTransportadora);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasIndex(nfe => nfe.NumeroNota).IsUnique();

            builder.HasOne(nfe => nfe.Cliente)
                 .WithMany(c => c.NotasSaida)
                 .HasForeignKey(nfe => nfe.ClienteId);

            builder.HasOne(nfe => nfe.Fornecedor)
                 .WithMany(f => f.NotasSaida)
                 .HasForeignKey(nfe => nfe.FornecedorId);

            builder.HasOne(nfe => nfe.Transportadora)
                 .WithMany(f => f.NotasSaida)
                 .HasForeignKey(nfe => nfe.TransportadoraId);

            builder.ToTable("NotasSaida");
        }
    }
}
