using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Motocycle.Infra.CrossCutting.Commons.HttpFactory.Types;

namespace Motocycle.Infra.CrossCutting.Commons.HttpFactory.Interfaces
{
    public interface IHttpFactoryService
    {
        public Task<HttpResponseMessage> ExecuteGetRequestAsync(string url, string resource = null, (string UserName, string Password) basicAuth = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null);
        public Task<HttpResponseMessage> ExecuteGetRequestAsync(string url, string resource = null, string basicAuthBase64 = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null);
        public Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url, string resource = null, HttpContent body = null, (string UserName, string Password) basicAuth = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null);
        public Task<HandledResponse> ExecutePostBasicAuthRequestAsync(string url, string resource = null, HttpContent body = null, string basicAuthBase64 = default, IDictionary<string, string> queryParams = null, IDictionary<string, string> headerParams = null, bool rethrowsException = true);
        public HttpClient CreateFactoryClient(string url, (string UserName, string Password) basicAuth = default, int timeout = 0, IDictionary<string, string> header = null);
        public HttpClient CreateFactoryClient(string url, string basicAuthBase64, int timeout = 0, IDictionary<string, string> header = null);
        public HttpRequestMessage CreateFactoryRequest(string resource, HttpMethod method, HttpContent body = null, IDictionary<string, string> queryParams = null, IDictionary<string, string> header = null);
        public Task<HttpResponseMessage> ExecuteRequestAsync(HttpClient client, HttpRequestMessage request);
    }
}
