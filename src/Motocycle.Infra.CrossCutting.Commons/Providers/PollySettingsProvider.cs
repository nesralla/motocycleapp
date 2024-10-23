namespace Motocycle.Infra.CrossCutting.Commons.Providers
{
    public class PollySettingsProvider
    {
        public int Attempts { get; set; }
        public int MinDelay { get; set; }
        public int MaxDelay { get; set; }
    }
}
