using Motocycle.Domain.Core.Messages;

namespace Motocycle.Application.UseCases.ApiErrorLog.Response
{
    public class ApiErrorLogResponse : ResponseBase
    {
        public string RootCause { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string Type { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}
