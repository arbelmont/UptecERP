using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal
{
    public class NotaSaidaItensConfig : IEntityTypeConfiguration<NotaSaidaItens>
    {
        public void Configure(EntityTypeBuilder<NotaSaidaItens> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Codigo)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaidaItens.CodigoMaxLenght})");

            builder.Property(i => i.Descricao)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaidaItens.DescricaoMaxLenght})");

            builder.Property(i => i.Cfop)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaidaItens.CfopMaxLenght})");

            builder.Property(i => i.Ncm)
                .IsRequired()
                .HasColumnType($"varchar({NotaSaidaItens.NcmMaxLenght})");

            builder.Property(i => i.ValorUnitario)
                 .IsRequired()
                 .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaBaseCalculo)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaIcms)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaIpi)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaIva)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaPis)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.AliquotaCofins)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorBaseCalculo)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorIcms)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorPis)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorCofins)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorIpi)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.ValorTotal)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.Quantidade)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Ignore(nfe => nfe.Validation);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasOne(i => i.NotaSaida)
                .WithMany(nfe => nfe.Itens)
                .HasForeignKey(i => i.NotaSaidaId);

            builder.ToTable("NotaSaidaItens");
        }
    }
}
