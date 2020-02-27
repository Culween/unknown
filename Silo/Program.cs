using Grains;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Orleans.Runtime;
using Component;

namespace Silo
{
    class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("\n\n Press Enter to terminate...\n\n");
                Console.ReadLine();

                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .AddMemoryGrainStorage("store1")
                .ConfigureApplicationParts(
                    parts => parts
                        .AddApplicationPart(typeof(TaskGrain).Assembly)
                        .AddApplicationPart(typeof(TaskGroupGrain).Assembly)
                        .AddApplicationPart(typeof(EverythingIsOkGrain).Assembly)
                        .WithReferences())
                .UseInMemoryReminderService()
                .ConfigureLogging(logging => logging.AddConsole())
                .ConfigureServices((context, servicCollection) =>
                {
                    servicCollection.AddTransient<MyComponent>(sp => MyComponent.Create(sp.GetRequiredService<IGrainActivationContext>()));
                });

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
