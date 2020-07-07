using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ComponenteAlias = Uptec.Erp.Producao.Domain.Componentes.Models.Componente;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Componente
{
    public class ComponenteConfig : IEntityTypeConfiguration<ComponenteAlias>
    {
        public void Configure(EntityTypeBuilder<ComponenteAlias> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .HasColumnType($"varchar({ComponenteAlias.CodigoMaxLenght})")
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasColumnType($"varchar({ComponenteAlias.DescricaoMaxLenght})")
                .IsRequired();

            builder.Property(c => c.Preco)
                .HasColumnType($"decimal(18,4)")
                .IsRequired();

            builder.Property(c => c.Ncm)
                .HasColumnType($"varchar({ComponenteAlias.NcmMaxLenght})")
                .IsRequired();

            builder.Property(c => c.Quantidade)
                .HasColumnType($"decimal(18,4)");

            builder.Property(c => c.QuantidadeMinima)
                .HasColumnType($"decimal(18,4)");

            builder.Ignore(c => c.Validation);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("Componentes");
        }
    }
}
