using Definitiva.Shared.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Events
{
    public class NotaSaidaAddedEvent : Event
    {
        public Guid NotaId { get; private set; }
        //public NotaSaida NotaSaida { get; protected set; }

        public NotaSaidaAddedEvent(Guid notaId)
        {
            NotaId = notaId;
        }
    }
}
