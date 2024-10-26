using System;
using MediatR;
//using SQS.ServiceBus;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;

namespace Motocycle.Application.MessageBroker.Base
{
    public abstract class ConsumerBase
    {
        private readonly int _waitTimeSeconds = 5;
        private readonly IServiceProvider _serviceProvider;
        private readonly QueuesProvider.QueuesConsumer _queues;
        protected IHandler<DomainNotification> Notifications { get; set; }

        protected ConsumerBase(IServiceProvider serviceProvider, QueuesProvider queues)
        {
            _queues = queues.Consumers;
            _serviceProvider = serviceProvider;
        }

        protected async Task Consume()
        {
            using var scope = _serviceProvider.CreateScope();
            var receiveEndpoint = scope.ServiceProvider.GetRequiredService<IReceiveEndpoint>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            Notifications = scope.ServiceProvider.GetRequiredService<IHandler<DomainNotification>>();

            try
            {
                await ProcessCreateProductAsync(receiveEndpoint, mediator);
            }
            catch (Exception e)
            {
                Notifications.LogError(e);
                Notifications.Handle(DomainNotification.Error("MessageBroker", e.Message));
            }
        }

        private async Task ProcessCreateProductAsync(IReceiveEndpoint receiveEndpoint, IMediator mediator)
        {
            var request = await receiveEndpoint.GetTopicMessage<PublishVinculatedWorkCardRequest>(_queues.VinculatedWorkCard, _waitTimeSeconds);
            if (request is not null)
                await mediator.Send(request.WorkCardInfo);
        }
    }
}