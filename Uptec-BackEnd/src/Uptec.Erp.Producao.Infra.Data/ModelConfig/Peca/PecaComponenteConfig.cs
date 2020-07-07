using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Peca
{
    public class PecaComponenteConfig : IEntityTypeConfiguration<PecaComponente>
    {
        public void Configure(EntityTypeBuilder<PecaComponente> builder)
        {
            builder.HasKey(pc => new { pc.PecaId, pc.ComponenteId });

            builder.HasOne(pc => pc.Peca)
                .WithMany(p => p.Componentes)
                .HasForeignKey(pc => pc.PecaId);

            builder.HasOne(pc => pc.Componente)
                .WithMany(c => c.Pecas)
                .HasForeignKey(pc => pc.ComponenteId);

            builder.Property(pc => pc.Quantidade)
                .HasColumnType($"decimal(18,4)");
        }
    }
}
