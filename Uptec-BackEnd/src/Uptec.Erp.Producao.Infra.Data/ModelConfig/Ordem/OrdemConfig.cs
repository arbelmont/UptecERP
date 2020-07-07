using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Lote
{
    public class OrdemConfig : IEntityTypeConfiguration<Ordem>
    {
        public void Configure(EntityTypeBuilder<Ordem> builder)
        {

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrdemNumero)
                .IsRequired();

            builder.Property(o => o.DataEmissao)
                .IsRequired();

            builder.Property(o => o.QtdeTotal)
                .IsRequired();

            builder.Property(p => p.CodigoPeca)
                .HasColumnType($"varchar({Ordem.CodigoMaxLenght})");

            builder.Property(p => p.DescricaoPeca)
                .HasColumnType($"varchar({Ordem.DescricaoMaxLenght})");

            builder.Ignore(o => o.Validation);

            builder.HasQueryFilter(o => !o.Deleted);

            builder.HasOne(o => o.Cliente)
                 .WithMany(c => c.Ordens)
                 .HasForeignKey(o => o.ClienteId);

            builder.HasOne(o => o.Fornecedor)
                 .WithMany(f => f.Ordens)
                 .HasForeignKey(o => o.FornecedorId);

            builder.ToTable("Ordens");
        }
    }
}
