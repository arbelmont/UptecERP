using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Componente
{
    public class ComponenteMovimentoConfig : IEntityTypeConfiguration<ComponenteMovimento>
    {
        public void Configure(EntityTypeBuilder<ComponenteMovimento> builder)
        {
            builder.HasKey(cm => cm.Id);

            builder.Property(cm => cm.Quantidade)
                .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(cm => cm.Data)
               .IsRequired();

            builder.Property(cm => cm.TipoMovimento)
               .IsRequired();

            builder.Property(cm => cm.PrecoUnitario)
               .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(cm => cm.PrecoTotal)
               .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(cm => cm.NotaFiscal)
               .HasColumnType($"varchar({ComponenteMovimento.NotaFiscalMaxLenght})");

            builder.Property(cm => cm.Saldo)
               .HasColumnType($"decimal(18,4)");

            builder.Property(cm => cm.Historico)
               .HasColumnType($"varchar({ComponenteMovimento.HistoricoMaxLenght})");

            builder.Ignore(cm => cm.Validation);

            builder.HasOne(cm => cm.Componente)
                   .WithMany(c => c.Movimentos)
                   .HasForeignKey(cm => cm.ComponenteId);

            builder.HasQueryFilter(cm => !cm.Deleted);

            builder.ToTable("ComponenteMovimentos");
        }
    }
}
