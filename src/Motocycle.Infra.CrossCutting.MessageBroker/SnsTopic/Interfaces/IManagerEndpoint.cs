using System.Threading.Tasks;
using System.Collections.Generic;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic.Interfaces
{
    public interface IManagerEndpoint
    {
        Task<List<QueueResponse>> ShowQueues();
        Task DeleteAllMessages(string endpoint);
    }
}
