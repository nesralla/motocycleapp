using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Diagnostics;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Interfaces;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Types;

namespace Motocycle.Infra.CrossCutting.Commons.HttpFactory.Services
{
    public class HttpFactoryService : IHttpFactoryService
    {
        private readonly HttpClient _clientFactory;
        private readonly ILogger<HttpFactoryService> _logger;

        public HttpFactoryService(IHttpClientFactory clientFactory, ILogger<HttpFactoryService> logger)
        {
            _clientFactory = clientFactory.CreateClient(typeof(HttpFactoryService).Name);
            _logger = logger;
        }

        public HttpClient CreateFactoryClient(string url, (string UserName, string Password) basicAuth = default, int timeout = 0, IDictionary<string, string> header = null)
        {
            _clientFactory.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (header is not null)
            {
                header.ToList().ForEach(x =>
                {
                    _clientFactory.DefaultRequestHeaders.Add(x.Key, x.Value);
                });
            }

            if (timeout > 0)
            {
                _clientFactory.Timeout = TimeSpan.FromSeconds(timeout);
            }

            _clientFactory.BaseAddress = new Uri(url);

            if (!basicAuth.Equals(default))
            {
                var byteArray = Encoding.ASCII.GetBytes($"{basicAuth.UserName}:{basicAuth.Password}");
                // _clientFactory.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
            return _clientFactory;
        }

        public HttpClient CreateFactoryClient(string url, string basicAuthBase64 = default, int timeout = 0, IDictionary<string, string> header = null)
        {
            _clientFactory.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (header is not null)
            {
                header.ToList().ForEach(x =>
                {
                    _clientFactory.DefaultRequestHeaders.Add(x.Key, x.Value);
                });
            }

            if (timeout > 0)
            {
                _clientFactory.Timeout = TimeSpan.FromSeconds(timeout);
            }

            _clientFactory.BaseAddress = new Uri(url);



            return _clientFactory;
        }

        public HttpRequestMessage CreateFactoryRequest(string resource, HttpMethod method, HttpContent body = null, IDictionary<string, string> queryParams = null, IDictionary<string, string> header = null)
        {
            var request = new HttpRequestMessage(method, resource);

            if (header is not null)
            {
                header.ToList().ForEach(x =>
                {
                    request.Headers.Add(x.Key, x.Value);
                });
            }

            if (queryParams is not null)
            {
                queryParams.ToList().ForEach(x =>
                {
                    resource = resource.Replace(x.Key, x.Value);
                });
            }

            if (body is not null)
            {
                request.Content = body;
            }

            return request;
        }

        public async Task<HttpResponseMessage> ExecuteGetRequestAsync(string url, string resource = null, (string UserName, string Password) basicAuth = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null)
        {
            var client = CreateFactoryClient(url, basicAuth);
            var request = CreateFactoryRequest(resource, HttpMethod.Get, null, queryParams, headerParams);

            return await ExecuteRequestAsync(client, request);
        }

        public async Task<HttpResponseMessage> ExecuteGetRequestAsync(string url, string resource = null, string basicAuthBase64 = null, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null)
        {
            var client = CreateFactoryClient(url, basicAuthBase64);
            var request = CreateFactoryRequest(resource, HttpMethod.Get, null, queryParams, headerParams);

            return await ExecuteRequestAsync(client, request);
        }

        public async Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url, string resource = null, HttpContent body = null, (string UserName, string Password) basicAuth = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null)
        {
            var client = CreateFactoryClient(url, basicAuth, 0, headerParams);
            var request = CreateFactoryRequest(resource, HttpMethod.Post, body, queryParams, headerParams);

            return await ExecuteRequestAsync(client, request);
        }

        public async Task<HandledResponse> ExecutePostBasicAuthRequestAsync(string url, string resource = null, HttpContent body = null, string basicAuthBase64 = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null, bool rethrowsException = true)
        {
            var client = CreateFactoryClient(url, basicAuthBase64, 0, headerParams);
            var request = CreateFactoryRequest(resource, HttpMethod.Post, body, queryParams, headerParams);

            HttpResponseMessage response = null;
            Exception exExec = null;
            try
            {
                response = await ExecuteRequestAsync(client, request);
            }
            catch (Exception ex)
            {
                exExec = ex;
                if (rethrowsException)
                    throw;
            }

            var requestHeaders = response?.RequestMessage?.Headers;
            int retryAttempt = requestHeaders?.Contains("NS-RETRY-ATTEMPTS") == true ? int.Parse(requestHeaders.GetValues("NS-RETRY-ATTEMPTS").First()) : 0;

            return new HandledResponse
            {
                Response = response,
                RetryAttempts = retryAttempt,
                Exception = exExec
            };
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(HttpClient client, HttpRequestMessage request)
        {

            _logger.LogInformation(JsonConvert.SerializeObject(new
            {
                Message = "Iniciando Request",
                request.Method,
                request.RequestUri
            }));
            HttpResponseMessage response = request.Method switch
            {
                HttpMethod m when m == HttpMethod.Post => await client.PostAsync(request.RequestUri, request.Content),
                HttpMethod m when m == HttpMethod.Put => await client.PutAsync(request.RequestUri, request.Content),
                HttpMethod m when m == HttpMethod.Delete => await client.DeleteAsync(request.RequestUri),
                _ => await client.GetAsync(request.RequestUri),
            };

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation(JsonConvert.SerializeObject(new
                {
                    Message = "Request finalizada.",
                    response.StatusCode,
                    request.Method,
                    request.RequestUri,

                }));
            }
            else
            {
                _logger.LogError(JsonConvert.SerializeObject(new
                {
                    response.StatusCode,
                    response.Headers,
                    request.RequestUri,
                    Body = response.Content.ReadAsStringAsync(),

                }));
            }

            return response;
        }
    }
}
