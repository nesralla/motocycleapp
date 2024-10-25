using System.Net;

namespace Motocycle.Api.Configurations.HealthChecks.Model
{
    public class ApiDependencyStatus
    {
        public bool IsOk { get; set; }
        public string Url { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
