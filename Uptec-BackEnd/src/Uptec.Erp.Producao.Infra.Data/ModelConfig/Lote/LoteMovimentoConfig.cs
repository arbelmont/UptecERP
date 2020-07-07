using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Lote
{
    public class LoteMovimentoConfig : IEntityTypeConfiguration<LoteMovimento>
    {
        public void Configure(EntityTypeBuilder<LoteMovimento> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LoteSequencia)
                .IsRequired();

            builder.Property(l => l.Data)
                .IsRequired();

            builder.Property(l => l.Quantidade)
                .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(l => l.PrecoUnitario)
                .HasColumnType($"decimal(18,4)");

            builder.Property(l => l.PrecoTotal)
                .HasColumnType($"decimal(18,4)");

            builder.Property(l => l.TipoMovimento)
               .IsRequired();

            builder.Property(cm => cm.NotaFiscal)
               .HasColumnType($"varchar({LoteMovimento.NotaFiscalMaxLenght})");

            builder.Property(cm => cm.Historico)
               .HasColumnType($"varchar({LoteMovimento.HistoricoMaxLenght})");

            builder.Ignore(cm => cm.Validation);

            builder.HasOne(l => l.Lote)
                .WithMany(m => m.Movimentos)
                .HasForeignKey(m => m.LoteId);
        }
    }
}
