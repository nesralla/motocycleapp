using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace Motocycle.Api.Controllers
{
    public class ApiErrorLogController : MainController
    {
        private readonly IMediator _mediator;

        public ApiErrorLogController(IHandler<DomainNotification> notifications, IMediator mediator) : base(notifications)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Errors Report
        /// </summary>
        /// <returns></returns>
        [HttpGet("{format}"), Authorize(Policy = PolicyProvider.AdministrationPolicy)]
        [SwaggerResponse(StatusCodes.Status200OK, null, typeof(byte[]))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, null, typeof(InternalValidationProblemDetails))]
        public async Task<IActionResult> GetAsync(ReportFormat format, [FromQuery] DateTime startDate, DateTime endDate)
        {
            var result = await _mediator.Send(new GetErrorsRequest { Format = format, StartDate = startDate, EndDate = endDate });
            return File(result.File, result.Format.GetDescription(), "ErrorsReport");
        }
    }
}