using System.Net;
using FluentValidation;
using TaskMgr.Application.Exceptions;

namespace TaskMgr.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occured");

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = exception switch
        {
            TaskEntityNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            ValidationException => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
        var errorResponse = new
        {
            StatusCode = response.StatusCode,
            Message = exception.Message
        };

        await response.WriteAsJsonAsync(errorResponse);
    }
}