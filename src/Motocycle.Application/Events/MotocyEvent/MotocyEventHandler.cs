using RabbitMQ.Client;
using Motocycle.Application.Events.Base;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Infra.CrossCutting.Commons.Providers;

namespace Motocycle.Application.Events.MotocyEvent
{
    public class MotocyEventHandler : EventHandlerBase<MotocyEvent>
    {

        private readonly IRecordedExchange _snsTopic;
        private readonly QueuesProvider.TopicProducer _topic;

        public MotocyEventHandler(
            IPublishTopic snsTopic,
            QueuesProvider topic,
            IUnitOfWork unitOfWork,
            IHandler<DomainNotification> notifications) : base(notifications, unitOfWork)
        {
            _snsTopic = snsTopic;
            _topic = topic.TopicProducers;
        }

        public override async Task Handle(MotocyEvent notification, CancellationToken cancellationToken)
        {
            Notifications.LogInfo($"[{nameof(MotocyEventHandler)}] -[{nameof(MotocyEvent)}] - Publish CardUserEvent with payload: {notification.ToJson()}");
            await _snsTopic.Publish(_topic.MotocycleEvent, notification);
        }
    }
}
