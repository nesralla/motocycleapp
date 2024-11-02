using System.Collections.Generic;

namespace Motocycle.Infra.CrossCutting.MessageBroker.SnsTopic
{
    public class QueueResponse
    {
        public string Url { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
