

using System;
using MediatR;

namespace Definitiva.Shared.Domain.Events
{
    public class Message : INotification
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}