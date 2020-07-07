using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Events
{
    public class NotaSaidaEventHandler : INotificationHandler<NotaSaidaAddedEvent>
    {
        private readonly IBus _bus;
        private readonly INotaSaidaService _notaSaidaService;
        private readonly INotaSaidaRepository _notaSaidaRepository;

        public NotaSaidaEventHandler(IBus bus,
                                     INotaSaidaService notaSaidaService, 
                                     INotaSaidaRepository notaSaidaRepository)
        {
            _bus = bus;
            _notaSaidaService = notaSaidaService;
            _notaSaidaRepository = notaSaidaRepository;
        }

        public Task Handle(NotaSaidaAddedEvent notaSaidaAddedEvent, CancellationToken cancellationToken)
        {
            var nota = _notaSaidaRepository.GetByIdWithAggregate(notaSaidaAddedEvent.NotaId);

            if(nota == null)
            {
                _bus.PublishEvent(new DomainNotification("", "Nota Inexistente"));
                return Task.CompletedTask;
            }

            nota.SetEnderecoDestinatario(_notaSaidaRepository.GetEndereco(nota.EnderecoId, nota.TipoDestinatario));

            if (nota.TransportadoraId != null)
                nota.SetEnderecoTransportadora(_notaSaidaRepository.GetEnderecoTransportadora(nota.TransportadoraId.Value));

            if(!_notaSaidaService.TryEnviar(nota, out var result))
                nota.SetStatus(StatusNfSaida.Rejeitada, $"{DateTime.Now.Date.ToString()} - {result.Codigo} - {result.Mensagem}");
            else
                nota.SetStatus(StatusNfSaida.Processando, string.Empty);

            _notaSaidaRepository.UpdateStatus(nota);

            return Task.CompletedTask;
        }
    }
}
