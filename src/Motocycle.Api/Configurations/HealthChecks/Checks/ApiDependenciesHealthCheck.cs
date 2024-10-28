using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Motocycle.Infra.CrossCutting.Commons.Extensions;
using Motocycle.Api.Configurations.HealthChecks.Model;

namespace Motocycle.Api.Configurations.HealthChecks.Checks
{
    public class ApiDependenciesHealthCheck : IHealthCheck
    {
        private readonly IList<Uri> _apiList;
        private readonly IHttpClientFactory _clientFactory;
        private readonly InternalIntegrationSettingsProvider _internalIntegrationSettings;

        public ApiDependenciesHealthCheck(
            InternalIntegrationSettingsProvider internalIntegrationSettings,
            IHttpClientFactory clientFactory)
        {
            _internalIntegrationSettings = internalIntegrationSettings;
            _clientFactory = clientFactory;
            _apiList = GetHealthCheckEndpoints();
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = _clientFactory.CreateClient();
            var dictionary = new Dictionary<string, object>();
            var allApisOk = true;
            var failedApis = 0;

            try
            {
                await _apiList.ForEachAsync(async url =>
                {
                    var response = await client.GetAsync(url.AbsoluteUri);
                    var status = new ApiDependencyStatus
                    {
                        Url = url.AbsoluteUri,
                        HttpStatusCode = response.StatusCode
                    };
                    if (response.StatusCode == HttpStatusCode.OK) status.IsOk = true;
                    else
                    {
                        failedApis++;
                        allApisOk = false;
                        status.IsOk = false;
                    }

                    dictionary.Add(GetAppName(url), status);
                });

                if (allApisOk) return HealthCheckResult.Healthy($"{_apiList.Count} dependency APIs - OK", dictionary);
                else return HealthCheckResult.Unhealthy($"{failedApis} of {_apiList.Count} dependency APIs - Failed", null, dictionary);
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Dependency APIs - Exception", e, dictionary);
            }
        }

        private static string GetAppName(Uri url)
        {
            var appName = url.AbsoluteUri.Split('/');

            return appName[appName.Length - 2];
        }

        private List<Uri> GetHealthCheckEndpoints()
        {
            var apiList = new List<Uri>();
            string helthResource = "/health";

            foreach (PropertyInfo property in _internalIntegrationSettings?.Resources?.GetType().GetProperties())
                apiList.Add(new Uri($"{_internalIntegrationSettings.Url}{property.GetValue(_internalIntegrationSettings.Resources)}{helthResource}"));

            return apiList;
        }
    }
}
