using MediatR;

namespace Motocycle.Domain.Core.Messages
{
    public abstract class CommandRequest<TResponse> : RequestBase, IRequest<TResponse>
    {

    }

}
