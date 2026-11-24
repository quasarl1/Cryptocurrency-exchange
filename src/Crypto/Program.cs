using System.Reflection.Metadata;
using CryptoExchange.Data;

// ==>read ENV<== 
using DotNetEnv;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using CryptoExchange;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.PostgreSql;
using CryptoExchange.Jobs;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var names = builder.Configuration.GetSection("Forbidden_names:Names").Get<string[]>() ?? [];

PublicName.LoadReserved(names);

builder.Host.UseSerilog((context, services, config) => config
    .ReadFrom.Configuration(context.Configuration)   //different levels
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console() 
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,        // create new files every day
        retainedFileCountLimit: 14));   //how much files


builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(c => c.UseNpgsqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddTransient<ProccessOutBoxJob>();

builder.Services.AddHangfireServer();

static string RequireEnv(string name) =>
    Environment.GetEnvironmentVariable(name)
    ?? throw new InvalidOperationException($"Environment variable '{name}' is not set");

Env.Load("Docker-compose.env");

// TAKING FROM ===>DOCKER.ENV<===
var host = RequireEnv("DB_HOST");
var port = RequireEnv("DB_PORT");
var db_ = RequireEnv("DB_NAME");
var user = RequireEnv("DB_USER");
var pass = RequireEnv("DB_PASSWORD");

var connectionString = $"Host={host};Port={port};Database={db_};Username={user};Password={pass}";




builder.Services.AddControllers();


builder.Services.AddDbContext<DataBase>(options => 
    options.UseNpgsql(connectionString));


builder.Services.AddCors(options =>
{
   options.AddPolicy("AllowAll", policy =>
   {
       policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
   }) ;


//validation + creating a service

builder.Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection(JwtOptions.SelectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();


var secretKey = builder.Configuration["Jwt:Key"];

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        }; 
    });

});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    RecurringJob.AddOrUpdate<ProccessOutBoxJob>(
        "process-outbox",
        job => job.ExecuteAsync(CancellationToken.None),
        Cron.Minutely);
}
app.UseSerilogRequestLogging();
// app.UseMiddleware<Checker>(); //add #1
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.Run();