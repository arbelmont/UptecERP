using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal.Integracao.Receita
{
    public class ManifestacaoConfig : IEntityTypeConfiguration<CabecalhoNfe>
    {
        public void Configure(EntityTypeBuilder<CabecalhoNfe> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasIndex(m => m.ChaveNfe).IsUnique();

            builder.Property(m => m.NomeEmitente)
                .HasColumnType($"varchar({CabecalhoNfe.NomeEmitenteMaxLenght})")
                .IsRequired();

            builder.Property(m => m.DocumentoEmitente)
                .HasColumnType($"varchar({CabecalhoNfe.DocumentoEmitenteMaxLenght})")
                .IsRequired();

            builder.Property(m => m.ChaveNfe)
                .HasColumnType($"varchar({CabecalhoNfe.ChaveNfeLenght})")
                .IsRequired();

            builder.Property(m => m.ValorTotal)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(m => m.DataEmissao)
                .IsRequired();

            builder.Property(m => m.Situacao)
                .IsRequired();

            builder.Property(m => m.ManifestacaoDestinatario);

            builder.Property(m => m.NfeCompleta)
                .IsRequired();

            builder.Property(m => m.TipoNfe)
                .IsRequired();

            builder.Property(m => m.Versao)
                .IsRequired();

            builder.Property(m => m.DigestValue)
                .HasColumnType($"varchar({CabecalhoNfe.DigestValueMaxLenght})")
                .IsRequired();

            builder.Property(m => m.NumeroCartaCorrecao);

            builder.Property(m => m.CartaCorrecao)
                .HasColumnType($"varchar({CabecalhoNfe.CartaCorrecaoMaxLenght})");

            builder.Property(m => m.DataCartaCorrecao);

            builder.Property(m => m.DataCancelamento);

            builder.Property(m => m.JustificativaCancelamento)
                .HasColumnType($"varchar({CabecalhoNfe.JustificativaCancelamentoMaxLenght})");

            builder.Property(m => m.DataInclusao)
                .IsRequired();

            builder.Property(m => m.DataManifestacao);

            builder.Ignore(nfe => nfe.Validation);
        }
    }
}
