using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig
{
    public class TransportadoraTelefoneConfig : IEntityTypeConfiguration<TransportadoraTelefone>
    {

        public void Configure(EntityTypeBuilder<TransportadoraTelefone> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Numero)
                .HasColumnType($"varchar({Telefone.NumeroMaxLength})");

            builder.Property(t => t.Whatsapp)
                .HasDefaultValue(false);

            builder.Property(t => t.Observacoes)
                .HasColumnType($"varchar({Telefone.ObservacoesMaxLength})");

            builder.Property(t => t.Contato)
                .HasColumnType($"varchar({Telefone.ContatoMaxLength})");

            builder.Ignore(d => d.Validation);
            builder.Ignore(d => d.Obrigatorio);

            builder.HasOne(t => t.Transportadora)
                   .WithMany(e => e.Telefones)
                   .IsRequired();

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("TransportadoraTelefones");
        }
    }
}
