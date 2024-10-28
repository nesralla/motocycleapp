using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Motocycle.Api.Configurations.HealthChecks.Checks;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Api.Configurations.HealthChecks.Model;
using Motocycle.Infra.CrossCutting.Commons.Extensions;

namespace Motocycle.Api.Configurations.HealthChecks
{
    public static class HealthCheckSetup
    {
        private static readonly string _healthChecksResource = "/healthchecks";

        public static void AddHealthCheckSetup(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddHealthChecks()
                    .AddCheck<ApiDependenciesHealthCheck>(nameof(ApiDependenciesHealthCheck), tags: new[] { _healthChecksResource })
                    .AddCheck<DatabaseHealthCheck>(nameof(DatabaseHealthCheck), tags: new[] { _healthChecksResource })
                    .AddCheck<AppHealthCheck>(nameof(AppHealthCheck), tags: new[] { AppProvider.HealthResource });
        }

        public static void UseAppHealthChecks(this IApplicationBuilder app)
        {
            app.UseHealthChecks(AppProvider.HealthResource, GetHealthOptions());
            app.UseHealthChecks(_healthChecksResource, GetHealthChecksOptions());
        }

        private static HealthCheckOptions GetHealthOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(AppProvider.HealthResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static HealthCheckOptions GetHealthChecksOptions()
        {
            return new HealthCheckOptions
            {
                Predicate = (check) => check.Tags.Contains(_healthChecksResource),
                AllowCachingResponses = false,
                ResponseWriter = GetResponseWriter()
            };
        }

        private static Func<HttpContext, HealthReport, Task> GetResponseWriter()
        {
            return async (c, r) =>
            {
                c.Response.ContentType = "application/json";

                var results = r.Entries.Select(pair =>
                {
                    return KeyValuePair.Create(pair.Key, new ResponseResults
                    {
                        Status = pair.Value.Status.ToString(),
                        Description = pair.Value.Description,
                        Duration = pair.Value.Duration.TotalSeconds.ToString() + "s",
                        ExceptionMessage = pair.Value.Exception is not null ? pair.Value.Exception.Message : null,
                        Data = pair.Value.Data
                    });
                }).ToDictionary(p => p.Key, p => p.Value);

                var result = new ResponseHealthCheck
                {
                    Status = r.Status.ToString(),
                    TotalDuration = r.TotalDuration.TotalSeconds.ToString() + "s",
                    Results = results
                };

                await c.Response.WriteAsync(result.ToJsonIndented());
            };
        }
    }
}
