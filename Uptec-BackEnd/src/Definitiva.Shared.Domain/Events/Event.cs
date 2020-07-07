using System;

namespace Definitiva.Shared.Domain.Events
{
    public class Event : Message
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}