using System;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Pecas.Models
{
    public class PecaComponente
    {
        protected PecaComponente()
        {
        }

        public PecaComponente(Guid pecaId, Guid componenteId, decimal quantidade)
        {
            PecaId = pecaId;
            ComponenteId = componenteId;
            Quantidade = quantidade;
        }
        public Guid PecaId { get; private set; }
        public Guid ComponenteId { get; private set; }
        public decimal Quantidade { get; private set; }

        public virtual Peca Peca { get; private set; }
        public virtual Componente Componente { get; private set; }
    }
}
