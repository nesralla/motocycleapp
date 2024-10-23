namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class LogSettingsProvider
    {
        public string Path { get; set; }
        public string Bucket { get; set; }
        public string AppName { get; set; }
        public string GroupName { get; set; }
    }
}

