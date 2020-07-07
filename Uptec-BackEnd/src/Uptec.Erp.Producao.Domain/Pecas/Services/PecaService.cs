using System;
using System.Linq;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Producao.Domain.Pecas.Validations;

namespace Uptec.Erp.Producao.Domain.Pecas.Services
{
    public class PecaService : BaseService, IPecaService
    {
        private readonly IPecaRepository _pecaRepository;

        public PecaService(IBus bus, IPecaRepository pecaRepository) : base(bus)
        {
            _pecaRepository = pecaRepository;
        }

        public void Add(Peca peca)
        {
            if(!ValidateEntity(peca)) return;

            if(!ValidateBusinessRules(new PecaCanAddValidation(_pecaRepository).Validate(peca))) return;

            _pecaRepository.Add(peca);
        }

        public void Update(Peca peca)
        {
            if (!ValidateEntity(peca)) return;

            if (!ValidateBusinessRules(new PecaCanUpdateValidation(_pecaRepository).Validate(peca))) return;

            _pecaRepository.RemoverComponentesNaoInformados(peca);
            _pecaRepository.RemoverCodigosFornecedoresNaoInformados(peca);

            _pecaRepository.Update(peca);
        }

        public void Delete(Guid id)
        {
            var Peca = _pecaRepository.GetById(id);

            if (Peca == null)
            {
                NotifyError("Peça inexistente.");
                return;
            }

            Peca.Delete();
            _pecaRepository.Delete(Peca);
        }    

        public void Dispose()
        {
            _pecaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}