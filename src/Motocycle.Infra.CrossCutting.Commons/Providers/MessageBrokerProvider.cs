namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class MessageBrokerProvider
    {
        public string Profile { get; set; }
        public string Region { get; set; }
        public string Host { get; set; }
        public int TimeToDelay { get; set; }
    }
}
