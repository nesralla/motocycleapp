using System;
using System.Threading.Tasks;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.Interfaces.Base
{
    public interface IUseCaseBase<TRequest, TResponse> : IDisposable
        where TRequest : CommandRequest<TResponse>, new()
    {
        Task<TResponse> RegisterAsync(TRequest request);
    }
}
