using CoinApiClient;
using MagniseTaskBC;
using MagniseTaskBE;
using MagniseTaskDAC;
using MagniseTaskWebApi.Middleware;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

// Add services to the container.

builder.Services.AddDbContext<MagniseTaskContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnection")));

builder.Configuration.GetSection(ApiOptions.OptionName).Bind(ApiClient.options);
builder.Configuration.GetSection(CryptoService.CryptoSetting.OptionName).Bind(CryptoService.options);

builder.Services.AddScoped<ICryptoService, CryptoService>();

var isDocker = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";//run in docker env
if (isDocker)
{
    builder.Services.AddSingleton<IMagniseTaskRepository, MockMagniseTaskRepository>();//fake repository (loading data from cache)
}
else
{
    builder.Services.AddScoped<IMagniseTaskRepository, MagniseTaskRepository>();//loading data from MSSQL server
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestLoggingMiddleware>();//request logging

app.UseExceptionHandler(a => a.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;

    var result = JsonSerializer.Serialize(new { error = exception.Message });
    context.Response.ContentType = "application/json";
    await context.Response.WriteAsync(result);
}));

app.Run();
