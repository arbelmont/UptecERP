using Microsoft.EntityFrameworkCore;
using ArquivoAlias = Uptec.Erp.Producao.Domain.Arquivos.Models.Arquivo;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Arquivo
{
    public class ArquivoConfig : IEntityTypeConfiguration<ArquivoAlias>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ArquivoAlias> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                .HasColumnType($"varchar({ArquivoAlias.NumeroNotaMaxLenght})")
                .IsRequired();

            builder.Property(a => a.Tamanho)
                .HasColumnType("int");

            builder.Property(a => a.Tipo)
                .HasColumnType($"varchar({ArquivoAlias.TipoArquivoMaxLenght})")
                .IsRequired();

            builder.Property(a => a.Dados)
                .HasColumnType("varchar(max)")
                .IsRequired();

            builder.Property(a => a.DataGravacao)
                .IsRequired();

            builder.Ignore(l => l.Validation);

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("Arquivos");
        }
    }
}
