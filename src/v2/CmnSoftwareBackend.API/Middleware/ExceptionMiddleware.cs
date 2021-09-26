using System;
using System.Text.Json;
using System.Threading.Tasks;
using CmnSoftwareBackend.Shared.Entities.ComplexTypes;
using CmnSoftwareBackend.Shared.Utilities.Exceptions;
using CmnSoftwareBackend.Shared.Utilities.Results.ComplexTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;


namespace CmnSoftwareBackend.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                switch (ex)
                {
                    case NotFoundArgumentException error1:
                        await NotFoundArgumentAsync(context, error1);
                        break;
                    case NotFoundArgumentsException error2:
                        await NotFoundArgumentsAsync(context, error2);
                        break;
                    case ValidationErrorsException error3:
                            await ValidationErrorsAsync(context, error3);
                        break;
                    default:
                        await GeneralException(context, ex);
                        break;
                }
            }
        }

        private async Task GeneralException(HttpContext context, Exception exception)
        {

            var problemDetails = new
            {
                ResultStatus = ResultStatus.Error,
                Message = exception.Message,
                Detail = exception.StackTrace,
                StatusCode = HttpStatusCode.InternalServerError,
                Href = context.Request.GetDisplayUrl()
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

        private async Task NotFoundArgumentAsync(HttpContext context, NotFoundArgumentException exception)
        {

            var problemDetails = new
            {
                ResultStatus = ResultStatus.Warning,
                Message = exception.Message,
                Error = exception.ValidationError,
                StatusCode = HttpStatusCode.BadRequest,
                Href = context.Request.GetDisplayUrl()
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

        private async Task NotFoundArgumentsAsync(HttpContext context, NotFoundArgumentsException exception)
        {

            var problemDetails = new
            {
                ResultStatus = ResultStatus.Warning,
                Message = exception.Message,
                Error = exception.ValidationErrors,
                StatusCode = HttpStatusCode.BadRequest,
                Href = context.Request.GetDisplayUrl()
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

        private async Task ValidationErrorsAsync(HttpContext context, ValidationErrorsException exception)
        {

            var problemDetails = new
            {
                ResultStatus = ResultStatus.Warning,
                Message = exception.Message,
                ValidationError = exception.ValidationErrors,
                StatusCode = HttpStatusCode.BadRequest,
                Href = context.Request.GetDisplayUrl()
            };

            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(problemDetails);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(result);
        }

    }
}

