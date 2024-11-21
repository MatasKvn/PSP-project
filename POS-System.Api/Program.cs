using POS_System.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureSwagger();
builder.Services.AddAuthentication();
builder.Services.AddApiServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddBusinessServices(builder.Configuration); // configuration may be removed if not used
builder.Services.AddDataServices(builder.Configuration);
builder.Services.AddControllers();

//builder.WebHost.UseUrls("http://localhost:3000");

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
app.UseCors();

app.UseRouting();
app.MapControllers();
app.UseAuthentication();
//app.UseAuthorization(); //Throws error if uncommented, NEED FIX

app.Run();