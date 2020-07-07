using Definitiva.Shared.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Uptec.Erp.Producao.Domain.Arquivos.Interfaces;
using Uptec.Erp.Producao.Domain.Arquivos.Services;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Services;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Services;
using Uptec.Erp.Producao.Domain.Fiscal.Events;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Services;
using Uptec.Erp.Producao.Domain.Fiscal.Services.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Services;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Services;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Services;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Services;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Services;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Producao.Infra.Data.Repository;
using Uptec.Erp.Producao.Infra.Data.UoW;
using Uptec.Erp.Producao.Infra.Integracao.NFe.IoC;

namespace Uptec.Erp.Producao.Infra.IoC
{
    public class ProducaoBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            IntegracaoNfeIoC.RegisterServices(services);
            TrackerIoC.RegisterServices(services);

            //Data
            services.AddScoped<IArquivoRepository, ArquivoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IComponenteRepository, ComponenteRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IPecaRepository, PecaRepository>();
            services.AddScoped<ILoteRepository, LoteRepository>();
            services.AddScoped<INotaEntradaRepository, NotaEntradaRepository>();
            services.AddScoped<INotaSaidaRepository, NotaSaidaRepository>();
            services.AddScoped<IOrdemRepository, OrdemRepository>();
            services.AddScoped<ITransportadoraRepository, TransportadoraRepository>();
            services.AddScoped<ICabecalhoNfeRepository, CabecalhoNfeRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWok>();

            services.AddScoped<ProducaoContext>();

            //Domain
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IComponenteService, ComponenteService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IPecaService, PecaService>();
            services.AddScoped<ILoteService, LoteService>();
            services.AddScoped<INotaEntradaService, NotaEntradaService>();
            services.AddScoped<INotaSaidaService, NotaSaidaService>();
            services.AddScoped<INotaSaidaEmissao, NotaSaidaEmissao>();
            services.AddScoped<IOrdemService, OrdemService>();
            services.AddScoped<ITransportadoraService, TransportadoraService>();
            services.AddScoped<IManifestacaoNfeService, ManifestacaoNfeService>();

            services.AddScoped<INotificationHandler<NotaSaidaAddedEvent>, NotaSaidaEventHandler>();

        }
    }
}