using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;

namespace ClientApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            Configuration = configuration;
            Logger = logger;
        }

        public IConfiguration Configuration { get; }

        public ILogger Logger { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterOrleansClient(services);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var clusterClient = app.ApplicationServices.GetService<IClusterClient>();
            ConnectToOrleans(clusterClient);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void RegisterOrleansClient(IServiceCollection services)
        {
            IClusterClient client = new ClientBuilder()
                .UseLocalhostClustering()
                 .Configure<ClusterOptions>(options =>
                 {
                     options.ClusterId = "dev";
                     options.ServiceId = "OrleansBasics";
                 })
                 .Build();

            services.AddSingleton(client);
        }

        private void ConnectToOrleans(IClusterClient clusterClient)
        {
            clusterClient.Connect(async ex =>
            {
                await Task.Delay(1000);
                Logger.LogError(ex, "Connect Orleans erro.");
                return true;
            }).Wait();
            Logger.LogInformation("Client successfully connected to silo host.");
        }
    }
}
