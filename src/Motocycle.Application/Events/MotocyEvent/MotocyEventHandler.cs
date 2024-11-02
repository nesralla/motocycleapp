using Motocycle.Application.Events.Base;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using SQS.ServiceBus;

namespace Motocycle.Application.Events.MotocyEvent
{
    public class MotocyEventHandler : EventHandlerBase<MotocyEvent>
    {

        private readonly IPublishEndpoint _bus;
        private readonly QueuesProvider.QueuesProducer _queue;

        public MotocyEventHandler(
            IPublishEndpoint bus,
            QueuesProvider queue,
            IUnitOfWork unitOfWork,
            IHandler<DomainNotification> notifications) : base(notifications, unitOfWork)
        {
            _bus = bus;
            _queue = queue.Producers;
        }

        public override async Task Handle(MotocyEvent notification, CancellationToken cancellationToken)
        {
            Notifications.LogInfo($"[{nameof(MotocyEventHandler)}] -[{nameof(MotocyEvent)}] - Publish motocycle with payload: {notification.ToJson()}");
            await _bus.Publish(_queue.CreateMotocycleSender, notification);
        }
    }
}
