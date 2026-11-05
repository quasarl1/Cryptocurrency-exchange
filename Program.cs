using CryptoExchange.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Host.UseSerilog((context, services, config) => config
    .ReadFrom.Configuration(context.Configuration)   //levels
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console() 
    .WriteTo.File(
        path: "logs/log-.txt",
        rollingInterval: RollingInterval.Day,        // create new files every day
        retainedFileCountLimit: 14));   //how much


static string RequireEnv(string name) =>
    Environment.GetEnvironmentVariable(name)
    ?? throw new InvalidOperationException($"Environment variable '{name}' is not set");

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


});
var app = builder.Build();
app.UseSerilogRequestLogging();
// app.UseMiddleware<Checker>(); //add #1
app.MapControllers();