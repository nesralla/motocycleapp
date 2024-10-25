using System;
using System.Threading.Tasks;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.Interfaces.Base
{
    public interface IEventHandlerBase<in TRequest> : IDisposable
        where TRequest : EventRequest, new()
    {
        Task ProcessEventAsync(TRequest request);
    }
}
