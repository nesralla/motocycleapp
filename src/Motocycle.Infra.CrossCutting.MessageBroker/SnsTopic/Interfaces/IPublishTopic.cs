namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces;

public interface IPublishTopic
{
    Task Publish(string endpoint, object message);
}