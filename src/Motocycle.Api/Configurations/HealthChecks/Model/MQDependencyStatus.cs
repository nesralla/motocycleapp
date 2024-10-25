namespace Motocycle.Api.Configurations.HealthChecks.Model
{
    public class MQDependencyStatus
    {
        public bool IsOk { get; set; }
        public int ConsumerCount { get; set; }
    }
}
