using System.Threading.Tasks;
using System.Collections.Generic;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces
{
    public interface IPublishEndpoint
    {
        Task Publish(string endpoint, object message);
        Task Publish(string endpoint, List<object> messages);
    }
}
