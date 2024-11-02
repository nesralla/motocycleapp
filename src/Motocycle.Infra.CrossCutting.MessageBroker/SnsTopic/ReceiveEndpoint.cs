using System;
using Amazon.SQS;
using System.Linq;
using Amazon.SQS.Model;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic
{
    public class ReceiveEndpoint : IReceiveEndpoint
    {
        private readonly ILogger _logger;
        private const int MaxMessages = 1;
        private readonly IAmazonSQS _sqsClient;
        private readonly MessageBrokerProvider _messageBrokerSettings;

        public ReceiveEndpoint(
            IAmazonSQS sqsClient,
            ILogger<ReceiveEndpoint> logger,
            MessageBrokerProvider messageBrokerSettings
            )
        {
            _logger = logger;
            _sqsClient = sqsClient;
            _messageBrokerSettings = messageBrokerSettings;
        }

        public async Task<T> GetMessage<T>(string endpoint, int waitTime = 0)
        {
            var (IsSucces, Result) = await GetMessage(endpoint, waitTime);

            if (IsSucces)
                return Result.ToObject<T>();

            return default;
        }

        public async Task<T> GetTopicMessage<T>(string endpoint, int waitTime = 0)
        {
            var (IsSucces, Result) = await GetMessage(endpoint, waitTime);

            if (IsSucces)
                return Result.ToObject<SqsTopicMessage>().Message.ToObject<T>();

            return default;
        }

        private async Task<(bool IsSuccess, string Result)> GetMessage(string endpoint, int waitTime = 0)
        {
            var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}/{endpoint}";
            var result = await _sqsClient.ReceiveMessageAsync(new ReceiveMessageRequest
            {
                QueueUrl = url,
                MaxNumberOfMessages = MaxMessages,
                WaitTimeSeconds = waitTime
            });

            if (result.Messages.Any())
            {
                var (Success, Body) = ProcessMessage(result.Messages.FirstOrDefault());
                if (Success)
                    await DeleteMessage(result.Messages.FirstOrDefault(), url);

                return (true, Body);
            }

            return (false, default);
        }

        private async Task DeleteMessage(Message message, string url)
        {
            _logger.LogInformation($"\nDeleting message {message.MessageId} from queue...");
            await _sqsClient.DeleteMessageAsync(url, message.ReceiptHandle);
        }

        private (bool Success, string Body) ProcessMessage(Message message)
        {
            _logger.LogInformation($"Message Received at {DateTime.UtcNow} - {message.MessageId}: {message.Body}");
            return (true, message.Body);
        }

        private class SqsTopicMessage
        {
            public string Message { get; set; }
        }
    }
}
