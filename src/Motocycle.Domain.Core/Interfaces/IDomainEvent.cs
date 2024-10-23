using System;

namespace Motocycle.Domain.Core.Interfaces
{
    public interface IDomainEvent
    {
        int Version { get; }

        DateTime OccurrenceDate { get; }
    }
}
