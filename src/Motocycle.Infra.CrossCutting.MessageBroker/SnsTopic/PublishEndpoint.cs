using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic
{
    public class PublishEndpoint : IPublishEndpoint
    {
        private readonly IAmazonSQS _sqsClient;
        private readonly ILogger _logger;
        private readonly MessageBrokerProvider _messageBrokerSettings;

        public PublishEndpoint(
            IAmazonSQS sqsClient,
            ILogger<PublishEndpoint> logger,
            MessageBrokerProvider messageBrokerSettings)
        {
            _sqsClient = sqsClient;
            _logger = logger;
            _messageBrokerSettings = messageBrokerSettings;
        }

        public async Task Publish(string endpoint, object message)
        {
            var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}/{endpoint}";
            _logger.LogInformation($"Publish message in queue: {url}");
            SendMessageResponse responseSendMsg = await _sqsClient.SendMessageAsync(url, message.ToJson());
            _logger.LogInformation($"Message added to queue {url} with payload: {message.ToJson()}");
            _logger.LogInformation("HttpStatusCode", responseSendMsg.HttpStatusCode);
        }

        public async Task Publish(string endpoint, List<object> messages)
        {
            var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}/{endpoint}";
            List<SendMessageBatchRequestEntry> requestMessages = new List<SendMessageBatchRequestEntry>();

            messages.ForEach(item =>
            {
                requestMessages.Add(new SendMessageBatchRequestEntry { MessageBody = item.ToJson() });
            });

            _logger.LogInformation($"\nSending a batch of messages to queue  {url} with payload: {messages.ToJson()}");
            SendMessageBatchResponse responseSendBatch = await _sqsClient.SendMessageBatchAsync(url, requestMessages);

            foreach (SendMessageBatchResultEntry entry in responseSendBatch.Successful)
                _logger.LogInformation($"Message {entry.Id} successfully queued.");
        }
    }
}
