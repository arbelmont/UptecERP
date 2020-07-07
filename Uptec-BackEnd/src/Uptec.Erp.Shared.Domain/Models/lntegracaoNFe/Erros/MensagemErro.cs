using System.Collections.Generic;

namespace Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros
{
    public class MensagemErro
    {
        public string Codigo { get; set; }
        public string Mensagem { get; set; }
        public List<Erro> Erros { get; set; } = null;
    }
}
