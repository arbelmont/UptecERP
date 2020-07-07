using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal
{
    public class NotaEntradaItensConfig : IEntityTypeConfiguration<NotaEntradaItens>
    {
        public void Configure(EntityTypeBuilder<NotaEntradaItens> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Codigo)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntradaItens.CodigoMaxLenght})");

            builder.Property(i => i.Descricao)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntradaItens.DescricaoMaxLenght})");

            builder.Property(i => i.Cfop)
                .IsRequired()
                .HasColumnType($"varchar({NotaEntradaItens.CfopMaxLenght})");

            builder.Property(i => i.PrecoUnitario)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.PrecoTotal)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.Quantidade)
                .IsRequired()
                .HasColumnType($"decimal(18,4)");

            builder.Property(i => i.NumeroNotaCobertura)
                .HasColumnType($"varchar({NotaEntradaItens.NumeroNotaMaxLenght})");
            
            builder.Property(i => i.Localizacao)
                .HasColumnType($"varchar({NotaEntradaItens.LocalizacaoMaxLenght})");
            
            builder.Property(i => i.QtdeConcilia)
                .HasColumnType($"decimal(18,4)");

            builder.Ignore(nfe => nfe.Validation);
            builder.Ignore(nfe => nfe.CodigoCliente);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasOne(i => i.NotaEntrada)
                .WithMany(nfe => nfe.Itens)
                .HasForeignKey(i => i.NotaEntradaId);

            builder.ToTable("NotaEntradaItens");
        }
    }
}
