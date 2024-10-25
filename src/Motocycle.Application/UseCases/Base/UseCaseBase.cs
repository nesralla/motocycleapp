using MediatR;
using Motocycle.Application.UseCases.ApiErrorLog.Request;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Interfaces.Repositories.Base;
using System.Threading;
using System.Threading.Tasks;

namespace Motocycle.Application.UseCases.Base
{
    public abstract class UseCaseBase<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : CommandRequest<TResponse>, new()
    {
        protected bool ScapeError;
        protected readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        protected IHandler<DomainNotification> Notifications { get; }

        protected UseCaseBase(IHandler<DomainNotification> notifications, IUnitOfWork unitOfWork, IMediator mediator)
        {
            Notifications = notifications;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }

        protected bool Commit()
        {
            if (Notifications.HasNotifications())
                return false;

            return _unitOfWork.Commit();
        }

        protected async Task<bool> CommitAsync(bool forceCommit = false)
        {
            if (forceCommit)
                return await _unitOfWork.CommitAsync();

            if (Notifications.HasNotifications())
                return false;

            return await _unitOfWork.CommitAsync();
        }

        public abstract Task<TResponse> HandleSafeMode(TRequest request, CancellationToken cancellationToken);

        public virtual async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await HandleSafeMode(request, cancellationToken);

            if (Notifications.HasError() && !ScapeError)
                await _mediator.Send(new ApiErrorLogRequest
                {
                    RootCause = $"[ApplicationError]",
                    Message = Notifications.GetErrorMessages(),
                    Type = "Error",
                    ExceptionStackTrace = string.Empty
                }, cancellationToken);

            return result;
        }

        protected async Task SetLogInfo(string logInfo)
            => await Task.Run(() => Notifications.LogInfo(logInfo));
    }
}
