using System.Threading.Tasks;
using Definitiva.Shared.Domain.Commands;
using Definitiva.Shared.Domain.Events;

namespace Definitiva.Shared.Domain.Bus
{
    public interface IBus
    {
        Task PublishEvent<T>(T theEvent) where T : Event;
        Task SendCommand<T>(T theCommand) where T : Command;
    }
}