using System;
using FluentValidation;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemLoteValidation : AbstractValidator<OrdemLote>
    {
        public OrdemLoteValidation()
        {
            //TODO (criado automaticamente via gerador de codigo) Implemente sua lógica aqui e remova o exemplo abaixo
            /*
                RuleFor(m => m.nomeDaColuna)
                    .NotEmpty().WithMessage("Descrição não preenchida");
            */
        }
    }
}