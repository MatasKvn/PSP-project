using POS_System.Data;

var builder = WebApplication.CreateBuilder(args);

//You can add your own environment variable, which will hold your connection string without hardcoding it
// 1. Open command prompt
// 2. Write setx DATABASE_URL "<your_conn_string>"
// 3. Restart IDE
// var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

builder.Services.AddAuthentication();
builder.Services.AddDataServices(builder.Configuration);

// builder.Services
//     .AddIdentityApiEndpoints<IdentityUser>()
//     .AddEntityFrameworkStores<ApplicationDbContext>();
// builder.Services.AddDbContext<ApplicationDbContext<ApplicationUser<Guid>, IdentityRole<Guid>, Guid>>(options => options.UseNpgsql("Host=localhost;Port=5555;Username=postgres;Password=postgres;Database=postgres;", b => b.MigrationsAssembly("POS-System.Api")));
// builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.WebHost.UseUrls("http://localhost:3000");

// builder.Services.Configure<IdentityOptions>(options =>
// {
//     // Password settings.
//     options.Password.RequireDigit = true;
//     options.Password.RequireLowercase = true;
//     options.Password.RequireNonAlphanumeric = true;
//     options.Password.RequireUppercase = true;
//     options.Password.RequiredLength = 6;
//     options.Password.RequiredUniqueChars = 1;
//     // Lockout settings.
//     // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//     // options.Lockout.MaxFailedAccessAttempts = 5;
//     // options.Lockout.AllowedForNewUsers = true;
//     // User settings.
//     options.User.AllowedUserNameCharacters =
//     "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//     options.User.RequireUniqueEmail = false;
// });

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

app.UseAuthentication();
app.UseAuthorization();

app.Run();