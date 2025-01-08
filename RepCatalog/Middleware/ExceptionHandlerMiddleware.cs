using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RepCatalog.Middleware;

public class ExceptionHandlerMiddleware {
    private readonly RequestDelegate _next;
    public readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        } catch (Exception exception) {
            _logger.LogError(exception, "Uma falha aconteceu: {Message}", exception.Message);

            var problemDetails = new ProblemDetails {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Internal Server Error",
                Type = exception.GetType().Name,
                Detail = "Não foi possivel executar essa ação, tente novamente mais tarde!\n Se os problemas persistirem entre em contato com o suporte!"
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}
