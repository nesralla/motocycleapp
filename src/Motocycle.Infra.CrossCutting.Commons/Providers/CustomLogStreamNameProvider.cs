using System;

namespace Motocycle.Infra.CrossCutting.Commons.Providers
{

    public class CustomLogStreamNameProvider
    {
        public string GetLogStreamName()
        {
            var utcMinus3 = DateTime.UtcNow.AddHours(-3);
            return $"app.log_{utcMinus3:yyyy-MM-dd_HH-mm-ss}";
        }
    }
}
