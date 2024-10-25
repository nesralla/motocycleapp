using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.ApiErrorLog.Request
{
    public class ApiErrorLogRequest : CommandRequest<ApiErrorLogResponse>
    {
        public string RootCause { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
