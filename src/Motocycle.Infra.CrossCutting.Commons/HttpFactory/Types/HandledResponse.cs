using System;
using System.Net.Http;

namespace Motocycle.Infra.CrossCutting.Commons.HttpFactory.Types
{
    public class HandledResponse
    {
        public HttpResponseMessage Response { get; set; }
        public int RetryAttempts { get; set; }
        public Exception Exception { get; set; }
    }
}
