using Amazon.SimpleNotificationService;
using Microsoft.Extensions.Logging;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;
using MessageBrokerProvider = Motocycle.Infra.CrossCutting.Commons.Providers.MessageBrokerProvider;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic;

public class PublishTopic : IPublishTopic
{
    private readonly ILogger<PublishTopic> _logger;
    private readonly IAmazonSimpleNotificationService _snsClient;
    private readonly MessageBrokerProvider _messageBrokerSettings;

    public PublishTopic(
        ILogger<PublishTopic> logger,
        IAmazonSimpleNotificationService snsClient,
        MessageBrokerProvider messageBrokerSettings)
    {
        _snsClient = snsClient;
        _logger = logger;
        _messageBrokerSettings = messageBrokerSettings;
    }

    public async Task Publish(string endpoint, object message)
    {
        var url = $"{_messageBrokerSettings.Host.Replace("{region}", _messageBrokerSettings.Region)}:{endpoint}";
        _logger.LogInformation($"Publish message in topic: {url}");
        _logger.LogInformation($"Message added to topic {url} with payload: {message.ToJson()}");
        var response = await _snsClient.PublishAsync(url, message.ToJson());
        _logger.LogInformation("MessageId", response.MessageId);
        _logger.LogInformation("HttpStatusCode", response.HttpStatusCode);


    }
}