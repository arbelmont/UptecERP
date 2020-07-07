using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Componentes.Services
{
    public class ComponenteService : BaseService, IComponenteService
    {
        private readonly IComponenteRepository _componenteRepository;

        public ComponenteService(IBus bus, IComponenteRepository componenteRepository) : base(bus)
        {
            _componenteRepository = componenteRepository;
        }

        public void Add(Componente componente)
        {
            if(!ValidateEntity(componente)) return;

            if(!ValidateBusinessRules(new ComponenteCanAddValidation(_componenteRepository).Validate(componente))) return;

            var movimento = new ComponenteMovimento(Guid.NewGuid(), componente.Id, componente.Quantidade, TipoMovimentoEstoque.Entrada, componente.Preco, "", "Carga inicial do item.");
            movimento.SetSaldo(componente.Quantidade);

            _componenteRepository.Add(componente);
            _componenteRepository.AddMovimento(movimento);
        }

        public void Update(Componente componente)
        {
            if (!ValidateEntity(componente)) return;

            if (!ValidateBusinessRules(new ComponenteCanUpdateValidation(_componenteRepository).Validate(componente))) return;

            _componenteRepository.Update(componente);
        }

        public void Delete(Guid id)
        {
            var Componente = _componenteRepository.GetById(id);

            if (Componente == null)
            {
                NotifyError("Componente inexistente.");
                return;
            }

            Componente.Delete();
            _componenteRepository.Delete(Componente);
        }

        public void AddMovimentoEntrada(ComponenteMovimento movimento)
        {
            if (!ValidateEntity(movimento)) return;

            var componente = _componenteRepository.GetById(movimento.ComponenteId);

            componente.AddQuantidade(movimento.Quantidade);
            movimento.SetSaldo(componente.Quantidade);

            _componenteRepository.AddMovimento(movimento);
            _componenteRepository.Update(componente);
        }

        public void AddMovimentoSaida(ComponenteMovimento movimento)
        {
            if (!ValidateEntity(movimento)) return;

            var componente = _componenteRepository.GetById(movimento.ComponenteId);

            if (!ValidateBusinessRules(new ComponenteMovimentoCanAddSaidaValidation(componente).Validate(movimento))) return;

            componente.SubQuantidade(movimento.Quantidade);
            movimento.SetSaldo(componente.Quantidade);

            _componenteRepository.AddMovimento(movimento);

            _componenteRepository.UpdateSaldo(componente);

            //try
            //{
            //    _componenteRepository.UpdateSaldo(componente);
            //}
            //catch (Exception ex)
            //{
            //    var ee = ex.Message;
            //}
        }

        public void UpdateMovimento(ComponenteMovimento movimento)
        {
            if (!ValidateEntity(movimento)) return;

            _componenteRepository.UpdateMovimento(movimento);
        }

        public void Dispose()
        {
            _componenteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}