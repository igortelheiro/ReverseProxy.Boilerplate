using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Tracing.Butterfly;

namespace MGR.ApiGateway
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment host)
        {
            var env = host.EnvironmentName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(host.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"ocelot.{env}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddOcelot(Configuration);
                //.AddButterfly(option =>
                //{
                //    var butterflyUrl = Configuration.GetConnectionString("Butterfly");

                //    option.Service = "Ocelot";
                //    option.CollectorUrl = butterflyUrl;
                //});
        }


        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            await app.UseOcelot();
        }
    }
}