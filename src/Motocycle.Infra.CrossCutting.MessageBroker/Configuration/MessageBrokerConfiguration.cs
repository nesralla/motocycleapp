
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic;
using Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;

namespace Motocycle.Infra.CrossCutting.MessageBroker.Configuration;


public static class MessageBrokerConfiguration
{
    public static void AddAwsSnsConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services is null) throw new ArgumentNullException(nameof(services));
        if (configuration is null) throw new ArgumentNullException(nameof(configuration));

        services.AddAWSService<IAmazonSimpleNotificationService>();
        services.AddTransient<IPublishTopic, PublishTopic>();
    }
}