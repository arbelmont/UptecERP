using System;
using System.Collections.Generic;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemFinalizarViewModel
    {
        public Guid Id { get; set; }
        public List<OrdemLoteFinalizarViewModel> OrdemLotes { get; set; }
    }
}
