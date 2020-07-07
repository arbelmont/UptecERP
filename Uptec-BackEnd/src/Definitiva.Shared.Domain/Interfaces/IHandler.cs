using Definitiva.Shared.Domain.Events;

namespace Definitiva.Shared.Domain.Interfaces
{
    public interface IHandler<in T> where T : Message
    {
        void Handler(T message);
    }
}
