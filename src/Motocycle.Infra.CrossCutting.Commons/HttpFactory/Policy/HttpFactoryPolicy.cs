using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Motocycle.Infra.CrossCutting.Commons.Providers;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Motocycle.Infra.CrossCutting.Commons.HttpFactory.Policy
{
    public static class HttpFactoryPolicy
    {
        private static readonly string LINEBREAK = "\n\n\n\n\n\n\n\n\n";

        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(IServiceProvider services, HttpRequestMessage request)
        {
            var pollySettings = services.GetRequiredService<IOptions<PollySettingsProvider>>()?.Value;

            return
                HttpPolicyExtensions
                .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.UnavailableForLegalReasons)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.BadGateway)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.ServiceUnavailable)
                        .WaitAndRetryAsync(pollySettings.Attempts, ComputeDuration, (result, timeSpan, retryAttempt, pollyContext) =>
                                           {
                                               request.Headers.SetAttemptsHeader(retryAttempt.ToString());
                                               OnHttpRetry(result, timeSpan, retryAttempt, pollySettings.Attempts, services);
                                           });

        }

        private static void OnHttpRetry(DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryAttemp, int attempts, IServiceProvider services)
        {
            var logger = services.GetService<ILogger>();

            if (result.Result is not null)
            {
                logger.LogWarning($"{LINEBREAK}Request failed with {result.Result.StatusCode}. Waiting {timeSpan} before next retry. Retry attempt {retryAttemp}/{attempts}.{LINEBREAK}");
            }
            else
            {
                logger.LogWarning($"{LINEBREAK}Request failed because network failure. Waiting {timeSpan} before next retry. Retry attempt {retryAttemp}/{attempts}.{LINEBREAK}");
            }
        }

        private static TimeSpan ComputeDuration(int input)
        {
            return TimeSpan.FromSeconds(Math.Pow(2, input)) + TimeSpan.FromMilliseconds(new Random().Next(0, 100));
        }

        private static void SetAttemptsHeader(this HttpRequestHeaders headers, string attempts)
        {
            var headerKey = "NS-RETRY-ATTEMPTS";
            if (headers.Contains(headerKey)) headers.Remove(headerKey);
            headers.Add(headerKey, attempts);
        }
    }
}
