using System.Threading.Tasks;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces
{
    public interface IReceiveEndpoint
    {
        Task<T> GetMessage<T>(string endpoint, int waitTime = 0);
        Task<T> GetTopicMessage<T>(string endpoint, int waitTime = 0);
    }
}
