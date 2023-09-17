using Chat.Domain.Exceptions.ForbiddenException;
using Chat.Domain.Exceptions.NotFound;
using System.Text.Json;

namespace Chat.WebApi.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exception switch
            {
                NotFoundException => StatusCodes.Status404NotFound,
                InvalidDataException => StatusCodes.Status400BadRequest,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };

            var response = new
            {
                error = exception.Message
            };
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
