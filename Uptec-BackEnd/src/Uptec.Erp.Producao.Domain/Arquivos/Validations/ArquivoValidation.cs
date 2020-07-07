using System;
using FluentValidation;
using Uptec.Erp.Producao.Domain.Arquivos.Models;

namespace Uptec.Erp.Producao.Domain.Arquivos.Validations
{
    public class ArquivoValidation : AbstractValidator<Arquivo>
    {
        public ArquivoValidation()
        {
            RuleFor(a => a.Dados)
                .NotEmpty().WithMessage("Arquivo vazio! Necessário informar um arquivo com dados.");
        }
    }
}