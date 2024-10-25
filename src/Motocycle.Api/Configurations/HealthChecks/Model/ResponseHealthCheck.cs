using System.Collections.Generic;

namespace Motocycle.Api.Configurations.HealthChecks.Model
{
    public class ResponseHealthCheck
    {
        public string Status { get; set; }
        public string TotalDuration { get; set; }
        public Dictionary<string, ResponseResults> Results { get; set; }
    }
}
