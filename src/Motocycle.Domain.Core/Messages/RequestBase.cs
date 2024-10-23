using System;
using Newtonsoft.Json;

namespace Motocycle.Domain.Core.Messages
{
    public abstract class RequestBase
    {
        [JsonIgnore]
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;
    }

}
