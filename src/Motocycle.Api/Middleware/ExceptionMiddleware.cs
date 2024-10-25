using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Motocycle.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
                                        ILogger<ExceptionMiddleware> logger,
                                        IMediator mediator)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(ex, logger, mediator);
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await httpContext.Response.WriteAsJsonAsync(
                    new InternalValidationProblemDetails(
                        new Dictionary<string, string[]> { { "ERROR", new[] { "N�o foi poss�vel completar sua solicita��o, tente novamente mais tarde" } } }));
            }
        }

        private static async Task HandleExceptionAsync(Exception exception, ILogger<ExceptionMiddleware> logger, IMediator mediator)
        {
            try
            {
                logger.LogError(exception, exception.Message);

                var errorLog = new ApiErrorLogRequest
                {
                    RootCause = $"[ExceptionMiddleware]",
                    Message = exception.GetErrorMsg(),
                    Type = "Exception",
                    ExceptionStackTrace = exception.StackTrace,
                };

                await mediator.Send(errorLog);
            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, ex.Message);
            }
        }
    }

}
