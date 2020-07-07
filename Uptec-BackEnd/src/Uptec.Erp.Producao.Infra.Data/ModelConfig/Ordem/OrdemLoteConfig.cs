using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Lote
{
    public class OrdemLoteConfig : IEntityTypeConfiguration<OrdemLote>
    {
        public void Configure(EntityTypeBuilder<OrdemLote> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.LoteNumero)
                .IsRequired();

            builder.Property(o => o.LoteSequencia)
                .IsRequired();

            builder.Property(o => o.Qtde)
                .IsRequired();

            builder.Property(o => o.NotaFiscalSaida)
                .HasColumnType($"varchar({OrdemLote.NotaFiscalMaxLenght})");

            builder.Ignore(o => o.Validation);

            builder.HasQueryFilter(o => !o.Deleted);

            builder.HasOne(o => o.Lote)
                 .WithMany(l => l.OrdemLotes)
                 .HasForeignKey(o => o.LoteId);

            builder.HasOne(o => o.Ordem)
                 .WithMany(l => l.OrdemLotes)
                 .HasForeignKey(o => o.OrdemId);

            builder.ToTable("OrdensLotes");
        }
    }
}
