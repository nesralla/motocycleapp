using System;
using System.Threading;
using System.Threading.Tasks;
using SQS.ServiceBus.Providers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Motocycle.Application.MessageBroker.Base;
using Motocycle.Infra.CrossCutting.Commons.Providers;

namespace Motocycle.Application.MessageBroker
{
    public class Worker : ConsumerBase, IHostedService
    {
        private readonly ILogger _logger;
        private readonly SQS.ServiceBus.Providers.MessageBrokerProvider _messageBrokerSettings;
        private Timer _timer = null!;

        public Worker(ILogger<Worker> logger, SQS.ServiceBus.Providers.MessageBrokerProvider messageBrokerSettings, IServiceProvider serviceProvider, QueuesProvider queues)
            : base(serviceProvider, queues)
        {
            _logger = logger;
            _messageBrokerSettings = messageBrokerSettings;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Executing process {DateTime.UtcNow}");
            _timer = new Timer(ExecuteProcess, null, TimeSpan.Zero, TimeSpan.FromSeconds(_messageBrokerSettings.TimeToDelay));
            return Task.CompletedTask;
        }

        public void ExecuteProcess(object state)
        {
            _ = Consume();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
