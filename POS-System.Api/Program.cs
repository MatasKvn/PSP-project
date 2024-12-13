using POS_System.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder
    .ConfigureSwagger()
    .ConfigureValidators();

builder.Services.AddAuthentication();
builder.Services.AddApiServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddBusinessServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("http://localhost:3000");

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

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();