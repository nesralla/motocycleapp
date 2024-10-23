namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class InternalIntegrationSettingsProvider
    {
        public string Url { get; set; }
        public Resource Resources { get; set; }
        public AccessControlConfiguration AccessControlConfigurations { get; set; }

        public class AccessControlConfiguration
        {
            public string Domain { get; set; }
            public string ApiKey { get; set; }
        }

        public class Resource
        {
            public string Identity { get; set; }
        }
    }
}