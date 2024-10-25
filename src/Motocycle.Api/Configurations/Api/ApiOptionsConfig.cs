using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Motocycle.Api.Configurations.Api
{
    internal static class ApiOptionsConfig
    {
        public static void LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var appConfig = configuration.Get<AppConfig>();
            appConfig.DbSettings.ConnectionString = appConfig.DbSettings.ConnectionString.DbStringFormat(configuration["DATABASE_HOST"], configuration["DATABASE_USER"], configuration["DATABASE_PASSWORD"]);
            DomainNotificationHandler.ConfigureLog(appConfig.LogSettings);


            services.AddSingleton(typeof(QueuesProvider), appConfig.Queues);
            services.AddSingleton(typeof(DbSettingsProvider), appConfig.DbSettings);
            services.AddSingleton(typeof(MessageBrokerProvider), appConfig.MessageBrokerSettings);
            services.AddSingleton(typeof(InternalIntegrationSettingsProvider), appConfig.InternalIntegrationSettings);
        }
    }

    internal class AppConfig
    {
        public QueuesProvider Queues { get; set; } = new QueuesProvider();
        public DbSettingsProvider DbSettings { get; set; } = new DbSettingsProvider();
        public LogSettingsProvider LogSettings { get; set; } = new LogSettingsProvider();
        public MessageBrokerProvider MessageBrokerSettings { get; set; } = new MessageBrokerProvider();
        public InternalIntegrationSettingsProvider InternalIntegrationSettings { get; set; } = new InternalIntegrationSettingsProvider();
    }
}