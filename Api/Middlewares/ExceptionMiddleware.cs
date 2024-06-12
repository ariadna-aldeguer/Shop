using Api.Models;
using Azure.Core;
using Data.Exceptions;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (ex is not BaseCustomException customException)
                {
                    var request = httpContext.Request;
                    _logger.LogError(ex, "Request: Unhandled Exception for Request {Path} {@QueryString}",
                                   request.Path, request.QueryString);
                }

                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is BaseCustomException customException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
               context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }
            return context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                context.Response.StatusCode,
                Message = exception.Message
            }));
        }
    }
}
