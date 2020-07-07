using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada
{
    public class NotaEntradaTipoEmissorViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public TipoEmissor TipoEmissor { get; set; }
    }
}
