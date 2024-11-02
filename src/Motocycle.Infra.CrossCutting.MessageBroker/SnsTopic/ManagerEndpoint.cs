using Amazon.SQS;
using Amazon.SQS.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;
using Motocycle.Infra.CrossCutting.Commons.Providers;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic
{
    public class ManagerEndpoint : IManagerEndpoint
    {
        private readonly ILogger _logger;
        private readonly IAmazonSQS _sqsClient;
        private readonly MessageBrokerProvider _messageBrokerSettings;

        public ManagerEndpoint(
            IAmazonSQS sqsClient,
            ILogger<ManagerEndpoint> logger, MessageBrokerProvider messageBrokerSettings)
        {
            _logger = logger;
            _sqsClient = sqsClient;
            _messageBrokerSettings = messageBrokerSettings;
        }

        public async Task<List<QueueResponse>> ShowQueues()
        {
            ListQueuesResponse responseList = await _sqsClient.ListQueuesAsync(new ListQueuesRequest());

            List<QueueResponse> response = new List<QueueResponse>();

            foreach (string url in responseList.QueueUrls)
            {
                var attributes = new List<string> { QueueAttributeName.All };
                GetQueueAttributesResponse responseGetAtt = await _sqsClient.GetQueueAttributesAsync(url, attributes);
                var queue = new QueueResponse { Url = url };

                foreach (var att in responseGetAtt.Attributes)
                    queue.Properties.Add(att);
            }

            return response;
        }

        public async Task DeleteAllMessages(string endpoint)
        {
            var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}/{endpoint}";
            _logger.LogInformation($"\nPurging messages from queue\n  {url}...");

            PurgeQueueResponse responsePurge = await _sqsClient.PurgeQueueAsync(url);
            _logger.LogInformation($"HttpStatusCode: {responsePurge.HttpStatusCode}");
        }
    }
}
