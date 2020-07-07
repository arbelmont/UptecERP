using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PecaAlias = Uptec.Erp.Producao.Domain.Pecas.Models.Peca;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Peca
{
    public class PecaConfig : IEntityTypeConfiguration<PecaAlias>
    {
        public void Configure(EntityTypeBuilder<PecaAlias> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Codigo)
                .HasColumnType($"varchar({PecaAlias.CodigoMaxLenght})")
                .IsRequired();
            builder.Property(p => p.CodigoSaida)
                .HasColumnType($"varchar({PecaAlias.CodigoSaidaMaxLenght})")
                .IsRequired();

            builder.Property(p => p.Descricao)
                .HasColumnType($"varchar({PecaAlias.DescricaoMaxLenght})")
                .IsRequired();

            builder.Property(p => p.Preco)
                .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(p => p.PrecoSaida)
                .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(p => p.Ncm)
                .HasColumnType($"varchar({PecaAlias.NcmMaxLenght})")
                .IsRequired();

            builder.Property(p => p.Revisao)
                .HasColumnType($"varchar({PecaAlias.RevisaoMaxLenght})");

            builder.Ignore(p => p.Validation);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("Pecas");
        }
    }
}
