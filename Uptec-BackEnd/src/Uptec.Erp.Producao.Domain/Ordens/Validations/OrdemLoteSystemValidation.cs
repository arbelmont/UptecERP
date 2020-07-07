using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemLoteSystemValidation : AbstractValidator<OrdemLote>
    {
        public OrdemLoteSystemValidation()
        {
            RuleFor(l => l.LoteNumero)
                .GreaterThan(0).WithMessage("Lote não informado.");

            RuleFor(l => l.Qtde)
                .GreaterThan(0).WithMessage("Quantidade a produzir por lote deve ser superior a zero.");
        }
    }
}