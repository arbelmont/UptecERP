using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Uptec.Erp.Producao.Domain.Arquivos.Models;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Producao.Infra.Data.ModelConfig;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Arquivo;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Cliente;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Clientes;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Componente;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Fiscal.Integracao.Receita;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Fornecedor;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Fornecedores;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Lote;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Peca;
using Uptec.Erp.Producao.Infra.Data.ModelConfig.Transportadora;

namespace Uptec.Erp.Producao.Infra.Data.Context
{
    public class ProducaoContext : DbContext
    {
        public DbSet<Arquivo> Arquivos { get; set; }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteEndereco> ClienteEnderecos { get; set; }
        public DbSet<ClienteTelefone> ClienteTelefones { get; set; }

        public DbSet<Componente> Componentes { get; set; }
        public DbSet<ComponenteMovimento> ComponenteMovimentos { get; set; }

        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<FornecedorEndereco> FornecedorEnderecos { get; set; }
        public DbSet<FornecedorTelefone> FornecedorTelefones { get; set; }

        public DbSet<Lote> Lotes { get; set; }
        public DbSet<LoteMovimento> LoteMovimentos { get; set; }
        public DbSet<LoteSaldo> LoteSaldo { get; set; }

        public DbSet<CabecalhoNfe> CabecalhosNfe { get; set; }

        public DbSet<NotaEntrada> NotasEntrada { get; set; }
        public DbSet<NotaEntradaItens> NotaEntradaItens { get; set; }

        public DbSet<Ordem> Ordens { get; set; }
        public DbSet<OrdemLote> OrdemLotes { get; set; }
        public DbSet<LinhaProducao> LinhaProducao { get; set; }

        public DbSet<Peca> Pecas { get; set; }
        public DbSet<PecaComponente> PecaComponente { get; set; }
        public DbSet<PecaFornecedorCodigo> PecaFornecedorCodigo { get; set; }

        public DbSet<Transportadora> Transportadoras { get; set; }
        public DbSet<TransportadoraEndereco> TransportadoraEnderecos { get; set; }
        public DbSet<TransportadoraTelefone> TransportadoraTelefones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArquivoConfig());

            modelBuilder.ApplyConfiguration(new ClienteConfig());
            modelBuilder.ApplyConfiguration(new ClienteEnderecoConfig());
            modelBuilder.ApplyConfiguration(new ClienteTelefoneConfig());

            modelBuilder.ApplyConfiguration(new FornecedorConfig());
            modelBuilder.ApplyConfiguration(new FornecedorEnderecoConfig());
            modelBuilder.ApplyConfiguration(new FornecedorTelefoneConfig());

            modelBuilder.ApplyConfiguration(new TransportadoraConfig());
            modelBuilder.ApplyConfiguration(new TransportadoraEnderecoConfig());
            modelBuilder.ApplyConfiguration(new TransportadoraTelefoneConfig());

            modelBuilder.ApplyConfiguration(new LoteConfig());
            modelBuilder.ApplyConfiguration(new LoteMovimentoConfig());
            modelBuilder.HasSequence<int>("LoteSequence").StartsAt(1).IncrementsBy(1);

            modelBuilder.ApplyConfiguration(new NotaEntradaConfig());
            modelBuilder.ApplyConfiguration(new NotaEntradaItensConfig());

            modelBuilder.ApplyConfiguration(new OrdemConfig());
            modelBuilder.ApplyConfiguration(new OrdemLoteConfig());
            modelBuilder.HasSequence<int>("OrdemSequence").StartsAt(1).IncrementsBy(1);

            modelBuilder.ApplyConfiguration(new PecaConfig());
            modelBuilder.ApplyConfiguration(new PecaComponenteConfig());
            modelBuilder.ApplyConfiguration(new PecaFornecedorCodigoConfig());

            modelBuilder.ApplyConfiguration(new ComponenteConfig());
            modelBuilder.ApplyConfiguration(new ComponenteMovimentoConfig());

            modelBuilder.HasSequence<int>("NotaSaidaSequence").StartsAt(10000).IncrementsBy(1);
            modelBuilder.ApplyConfiguration(new NotaSaidaConfig());
            modelBuilder.ApplyConfiguration(new NotaSaidaItensConfig());

            modelBuilder.ApplyConfiguration(new ManifestacaoConfig());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                .UseSqlServer(config.GetConnectionString("DefaultConnection"), opt => { opt.UseRowNumberForPaging(); })
                .EnableSensitiveDataLogging(true);
            
            base.OnConfiguring(optionsBuilder);
        }
    }
}