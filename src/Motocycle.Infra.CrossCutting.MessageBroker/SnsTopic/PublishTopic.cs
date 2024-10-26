using Microsoft.Extensions.Logging;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic;

public class PublishTopic : IPublishTopic
{
    private readonly ILogger<PublishTopic> _logger;
    //private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly MessageBrokerProvider _messageBrokerSettings;

    public PublishTopic(
        ILogger<PublishTopic> logger,
        MessageBrokerProvider messageBrokerSettings)
    {
        // _snsClient = snsClient;
        _logger = logger;
        _messageBrokerSettings = messageBrokerSettings;
    }

    public async Task Publish(string endpoint, object message)
    {
        var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}:{endpoint}";
        _logger.LogInformation($"Publish message in topic: {url}");
        // var response = await _snsClient.PublishAsync(url, message.ToJson());
        _logger.LogInformation($"Message added to topic {url} with payload: {message.ToJson()}");
        // _logger.LogInformation("HttpStatusCode", response.HttpStatusCode);
    }
}