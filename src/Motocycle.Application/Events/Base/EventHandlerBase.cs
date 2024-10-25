using MediatR;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Domain.Interfaces.Repositories.Base;

namespace Motocycle.Application.Events.Base
{
    public abstract class EventHandlerBase<TEvent> : INotificationHandler<TEvent>
        where TEvent : EventRequest, new()
    {
        protected IHandler<DomainNotification> Notifications { get; }
        private readonly IUnitOfWork _unitOfWork;

        protected EventHandlerBase(IHandler<DomainNotification> notifications, IUnitOfWork unitOfWork)
        {
            Notifications = notifications;
            _unitOfWork = unitOfWork;
        }

        protected bool Commit()
        {
            if (Notifications.HasNotifications())
                return false;

            return _unitOfWork.Commit();
        }

        protected async Task<bool> CommitAsync()
        {
            if (Notifications.HasNotifications())
                return false;

            return await _unitOfWork.CommitAsync();
        }

        public virtual Task Handle(TEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}
