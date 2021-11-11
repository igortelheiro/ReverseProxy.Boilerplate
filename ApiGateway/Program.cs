using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

builder.Configuration.AddJsonFile($"yarp.{builder.Environment.EnvironmentName}.json", false, true);


// Dependency Injection
var services = builder.Services;

//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        if (builder.Environment.IsDevelopment())
//            options.RequireHttpsMetadata = false;

//        options.Authority = builder.Configuration.GetConnectionString("LoginApi");
//    });

//services.AddAuthorization(options =>
//{
//    options.AddPolicy("teaApiPolicy", policy =>
//        policy.RequireAuthenticatedUser());
//});

services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));


// Pipeline
var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseEndpoints(endpoints =>
    endpoints.MapReverseProxy());


// Run
try
{
    Log.Information("Starting ApiGateway");
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