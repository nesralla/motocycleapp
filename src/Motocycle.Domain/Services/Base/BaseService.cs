

using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Services.Base;

namespace Motocycle.Domain.Services.Base
{
    public class BaseService : IBaseService
    {
        protected IHandler<DomainNotification> Notifications { get; }

        public BaseService(IHandler<DomainNotification> notifications)
        {
            Notifications = notifications;
        }
    }
}
