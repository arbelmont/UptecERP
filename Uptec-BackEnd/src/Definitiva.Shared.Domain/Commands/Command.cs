using System;
using Definitiva.Shared.Domain.Events;

namespace Definitiva.Shared.Domain.Commands
{
    public class Command : Message
    {
        public DateTime Timestamp { get; }

        public Command()
        {
            Timestamp = DateTime.Now;
        }
    }
}