using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Motocycle.Infra.CrossCutting.Commons.Middlewares;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"Iniciando requisição: {context.Request.Method} {context.Request.Path}");
        await _next(context);
        string message = $"Requisição concluída. StatusCode: {context.Response.StatusCode}";

        if (context.Response.StatusCode is < 200 or >= 300)
            _logger.LogError(message);
        else
            _logger.LogInformation(message);

    }
}
