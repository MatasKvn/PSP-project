using Microsoft.AspNetCore.Diagnostics;
using POS_System.Api.Configurations;
using POS_System.Api.ExceptionHandler;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureSwagger()
    .ConfigureValidators()
    .AddGlobalExceptionHandler();

builder.Services.AddAuthentication();
builder.Services.AddApiServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddBusinessServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseExceptionHandler(new ExceptionHandlerOptions
{
    ExceptionHandler = async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception != null)
        {
            var handler = context.RequestServices.GetRequiredService<GlobalExceptionHandler>();
            await handler.TryHandleAsync(context, exception, CancellationToken.None);
        }
    }
});

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();