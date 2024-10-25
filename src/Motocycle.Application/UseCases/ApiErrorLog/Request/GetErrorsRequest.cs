using System;
using Motocycle.Application.UseCases.ApiErrorLog.Response;
using Motocycle.Domain.Core.Messages;
using Motocycle.Domain.Enums;

namespace Motocycle.Application.UseCases.ApiErrorLog.Request
{
    public class GetErrorsRequest : CommandRequest<GetErrorsResponse>
    {
        public ReportFormat Format { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
