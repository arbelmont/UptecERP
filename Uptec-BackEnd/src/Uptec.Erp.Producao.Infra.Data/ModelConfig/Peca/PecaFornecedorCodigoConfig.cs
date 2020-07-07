using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Peca
{
    public class PecaFornecedorCodigoConfig : IEntityTypeConfiguration<PecaFornecedorCodigo>
    {
        public void Configure(EntityTypeBuilder<PecaFornecedorCodigo> builder)
        {
            builder.HasKey(pc => new { pc.PecaId, pc.FornecedorId });

            builder.HasOne(pc => pc.Peca)
                .WithMany(cf => cf.CodigosFornecedor)
                .HasForeignKey(pc => pc.PecaId);

            builder.HasOne(pc => pc.Fornecedor)
                .WithMany(cf => cf.PecaCodigosFornecedor)
                .HasForeignKey(pc => pc.FornecedorId);

            builder.Property(pc => pc.FornecedorCodigo)
                .HasColumnType($"varchar({PecaFornecedorCodigo.FornecedorCodigoMaxLenght})");
        }
    }
}
