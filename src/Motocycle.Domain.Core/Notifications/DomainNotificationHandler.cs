
using System;
using Serilog;
using System.Linq;
using Serilog.Filters;
using Serilog.Exceptions;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Motocycle.Domain.Core.Interfaces;
using Motocycle.Domain.Core.Notifications;
using Motocycle.Infra.CrossCutting.Commons.Providers;

namespace Motocycle.Domain.Core.Notifications
{
    public class DomainNotificationHandler : IHandler<DomainNotification>
    {
        private List<DomainNotification> _notifications;
        private readonly ILogger<DomainNotificationHandler> _logger;

        public DomainNotificationHandler(ILogger<DomainNotificationHandler> logger)
        {
            ClearNotifications();
            _logger = logger;
        }

        public void Handle(DomainNotification args)
        {
            if (!_notifications.Any(x => x.Value.Trim().ToUpper().Equals(args.Value.Trim().ToUpper())))
            {
                _notifications.Add(args);
            }
        }

        public virtual List<DomainNotification> GetNotifications()
        {
            return _notifications;
        }

        public virtual string GetNotificationMessages()
            => _notifications.Any() ? _notifications.Select(x => x.Value)?.Aggregate((current, next) => $"{current} : {next}") : string.Empty;

        public virtual string GetErrorMessages()
            => _notifications.Any() ? _notifications.Where(x => x.Type.Equals("Error")).Select(x => x.Value)?.Aggregate((current, next) => $"{current} : {next}") : string.Empty;

        public virtual string GetModelValidationMessages()
            => _notifications.Any() ? _notifications.Where(x => x.Type.Equals("ModelValidation")).Select(x => x.Value)?.Aggregate((current, next) => $"{current} : {next}") : string.Empty;

        public virtual IEnumerable<DomainNotification> Notify()
        {
            return GetNotifications();
        }

        public virtual bool HasNotifications()
        {
            return GetNotifications().Any();
        }

        public virtual bool HasError()
        {
            return GetNotifications().Any(x => x.Type.Equals("Error"));
        }

        public virtual bool HasModelValidation()
        {
            return GetNotifications().Any(x => x.Type.Equals("ModelValidation"));
        }

        public void ClearNotifications()
        {
            _notifications = new List<DomainNotification>();
        }

        public void LogInfo(string infoMessage)
        {
            _logger.LogInformation(infoMessage);
        }

        public void LogWarning(string warningMessage)
        {
            _logger.LogWarning(warningMessage);
        }

        public void LogError(string errorMessage)
        {
            _logger.LogError(errorMessage);
        }

        public void LogError(Exception ex)
        {
            _logger.LogError(ex, string.Empty);
        }

        public void LogError(Exception ex, string errorMessage)
        {
            _logger.LogError(ex, errorMessage);
        }

        public void LogFatal(string errorMessage)
        {
            _logger.LogCritical(errorMessage);
        }

        public void LogFatal(Exception ex)
        {
            _logger.LogCritical(ex, string.Empty);
        }

        public void LogFatal(Exception ex, string errorMessage)
        {
            _logger.LogCritical(ex, errorMessage);
        }

        public virtual Dictionary<string, string[]> GetNotificationsByKey()
        {
            var keys = _notifications.Select(s => s.Key).Distinct();
            var problemDetails = new Dictionary<string, string[]>();
            foreach (var key in keys)
            {
                problemDetails[key] = _notifications.Where(w => w.Key.Equals(key)).Select(s => s.Value).ToArray();
            }

            return problemDetails;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _notifications = null;
            ClearNotifications();
        }

        public static void ConfigureLog(LogSettingsProvider settings)
        {
            var logConfig = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", $"{settings.AppName}")
                .Enrich.WithExceptionDetails()
                .Enrich.WithCorrelationId()
                .Enrich.WithCorrelationIdHeader()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Verbose)
                .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Verbose)
                .WriteTo.Async(wt => wt.Console(
                    outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("Business error"))
                .Filter.ByExcluding(z => z.MessageTemplate.Text.Contains("healthcheck"));

            // ConfigureLogAmazonCloudWatch(ref logConfig, settings);
            // ConfigureLogAmazonS3(ref logConfig, settings);


            Log.Logger = logConfig.CreateLogger();
        }

    }
}
