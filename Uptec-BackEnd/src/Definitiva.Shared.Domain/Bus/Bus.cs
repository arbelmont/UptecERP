using System.Threading.Tasks;
using Definitiva.Shared.Domain.Commands;
using Definitiva.Shared.Domain.Events;
using MediatR;

namespace Definitiva.Shared.Domain.Bus
{
    public class Bus : IBus
    {
        private readonly IMediator _mediator;

        public Bus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task PublishEvent<T>(T theEvent) where T : Event
        {
            return Publish(theEvent);
        }

        public Task SendCommand<T>(T theCommand) where T : Command
        {
            return Publish(theCommand);
        }

        private Task Publish<T>(T mensagem) where T : Message
        {
            return _mediator.Publish(mensagem);
        }
    }
}