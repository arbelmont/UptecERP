using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoteAlias = Uptec.Erp.Producao.Domain.Lotes.Models.Lote;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Lote
{
    public class LoteConfig : IEntityTypeConfiguration<LoteAlias>
    {
        public void Configure(EntityTypeBuilder<LoteAlias> builder)
        {

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Data)
                .IsRequired();

            builder.Property(l => l.LoteNumero)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(l => l.Sequencia)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(l => l.Quantidade)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(l => l.Saldo)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(l => l.PrecoEntrada)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(l => l.CfopEntrada)
                .HasColumnType($"varchar({LoteAlias.CfopEntradaMaxLenght})");

            builder.Property(l => l.NotaFiscal)
               .HasColumnType($"varchar({LoteAlias.NotaFiscalMaxLenght})");

            builder.Property(l => l.NotaFiscalCobertura)
               .HasColumnType($"varchar({LoteAlias.NotaFiscalMaxLenght})");

            builder.Property(l => l.Localizacao)
               .HasColumnType($"varchar({LoteAlias.LocalizacaoMaxLenght})");

            builder.Property(l => l.QtdeConcilia)
                .HasColumnType("decimal(18,4)");

            builder.Property(l => l.EhCobertura)
                .HasDefaultValue(false);

            builder.Ignore(l => l.Validation);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.HasOne(l => l.Peca)
                 .WithMany(p => p.Lotes)
                 .HasForeignKey(l => l.PecaId);

            builder.HasOne(l => l.Cliente)
                 .WithMany(c => c.Lotes)
                 .HasForeignKey(l => l.ClienteId);

            builder.HasOne(l => l.Fornecedor)
                 .WithMany(f => f.Lotes)
                 .HasForeignKey(l => l.FornecedorId);

            builder.ToTable("Lotes");
        }
    }
}
