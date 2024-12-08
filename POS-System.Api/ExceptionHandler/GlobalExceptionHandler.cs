using POS_System.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Text.Json.Serialization;
using POS_System.Common;

namespace POS_System.Api.ExceptionHandler;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = exception is BaseException baseException
            ? baseException.GetErrorDetails()
            : new ErrorDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Details = exception.Message
            };

        httpContext.Response.StatusCode = problemDetails.Status;
        httpContext.Response.Headers.Append(HeaderNames.ContentType, MediaTypeNames.Application.Json);

        var jsonOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await httpContext.Response.WriteAsJsonAsync(problemDetails, jsonOptions, cancellationToken);

        return true;
    }
}