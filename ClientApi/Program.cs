using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ClientApi
{
    public class Program
    {
        public static IConfiguration Configuration { get; set; }


        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            CreateWebHost(args).Run();
        }

        public static IWebHost CreateWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .UseConfiguration(Configuration)
            .ConfigureLogging(logging => logging.AddConsole())
            .Build();
    }
}
