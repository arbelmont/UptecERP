using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Producao.Infra.Data.ModelConfig.Cliente
{
    public class ClienteTelefoneConfig : IEntityTypeConfiguration<ClienteTelefone>
    {

        public void Configure(EntityTypeBuilder<ClienteTelefone> builder)
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

            builder.HasOne(t => t.Cliente)
                  .WithMany(t => t.Telefones)
                  .IsRequired();

            builder.HasQueryFilter(t => !t.Deleted);

            builder.ToTable("ClienteTelefones");
        }
    }
}
