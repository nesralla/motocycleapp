using MediatR;
using Motocycle.Domain.Core.Messages;
using System;

namespace Motocycle.Domain.Core.Messages
{
    public abstract class EventRequest : RequestBase, INotification
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
