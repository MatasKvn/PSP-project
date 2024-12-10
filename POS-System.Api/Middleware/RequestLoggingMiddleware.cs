using System.Text;
using POS_System.Business.Logger;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<ApplicationLogger> logger)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.ContentLength > 0)
        {
            context.Request.EnableBuffering();
            var requestBody = await ReadRequestBody(context.Request);

            _logger.Log(LogLevel.Information, $"Request {context.Request.Method} {context.Request.Path}: {requestBody}");

            context.Request.Body.Position = 0;
        }

        await _next(context);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        string body = await reader.ReadToEndAsync();
        return body;
    }
}