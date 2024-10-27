using System;
using MediatR;
using SQS.ServiceBus;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Application.UseCases.Moto.Request;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Application.MessageBroker.Base
{
    public abstract class ConsumerBase(IServiceProvider serviceProvider, QueuesProvider queues)
    {
        private readonly int _waitTimeSeconds = 5;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private readonly QueuesProvider.QueuesConsumer _queues = queues.Consumers;
        protected IHandler<DomainNotification> Notifications { get; set; }

        protected async Task Consume()
        {
            using var scope = _serviceProvider.CreateScope();
            var receiveEndpoint = scope.ServiceProvider.GetRequiredService<IReceiveEndpoint>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            Notifications = scope.ServiceProvider.GetRequiredService<IHandler<DomainNotification>>();

            try
            {
                await ProcessCreateMotocyAsync(receiveEndpoint, mediator);
            }
            catch (Exception e)
            {
                Notifications.LogError(e);
                Notifications.Handle(DomainNotification.Error("MessageBroker", e.Message));
            }
        }

        private async Task ProcessCreateMotocyAsync(IReceiveEndpoint receiveEndpoint, IMediator mediator)
        {
            var request = await receiveEndpoint.GetTopicMessage<MotoRequest>(_queues.MotocycleProcess, _waitTimeSeconds);
            if (request is not null)
                _ = await mediator.Send(request);
        }
    }
}