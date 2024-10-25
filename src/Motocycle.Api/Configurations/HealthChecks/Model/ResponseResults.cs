using System.Collections.Generic;

namespace Motocycle.Api.Configurations.HealthChecks.Model
{
    public class ResponseResults
    {
        public string Status { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public string ExceptionMessage { get; set; }
        public IReadOnlyDictionary<string, object> Data { get; set; }
    }
}
