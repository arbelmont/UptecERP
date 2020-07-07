using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Arquivos.Interfaces;
using Uptec.Erp.Producao.Domain.Arquivos.Models;
using Uptec.Erp.Producao.Domain.Arquivos.Validations;

namespace Uptec.Erp.Producao.Domain.Arquivos.Services
{
    public class ArquivoService : BaseService, IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoService(IBus bus, IArquivoRepository arquivoRepository) : base(bus)
        {
            _arquivoRepository = arquivoRepository;
        }

        public void Add(Arquivo arquivo)
        {
            if(!ValidateEntity(arquivo)) return;
            arquivo.SetDataGravacao(DateTime.Now);

            _arquivoRepository.Add(arquivo);
        }

        public void Dispose()
        {
            _arquivoRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}