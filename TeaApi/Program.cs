using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((host, config) =>
{
    if (host.HostingEnvironment.IsDevelopment())
        config.MinimumLevel.Debug();

    config.MinimumLevel.Override("Microsoft", LogEventLevel.Information);
    config.Enrich.FromLogContext();
    config.WriteTo.Console();
});


// Dependency Injection
var services = builder.Services;

services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TeaApi", Version = "v1" }));
services.AddControllers();


// Pipeline
var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeaApi v1"));

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());


// Run
try
{
    Log.Information("Starting TeaApi");
    app.Run();
    Environment.Exit(0);
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    Environment.Exit(1);
}
finally
{
    Log.CloseAndFlush();
}