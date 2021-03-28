using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Hsf.ApplicatonProcess.August2020.Web.ConfigurationSetup;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Hsf.ApplicatonProcess.August2020.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Verbose()
                   .WriteTo.Console()
                   .WriteTo.File("Logs\\Hsf_Application.log", rollingInterval: RollingInterval.Day)
                   .CreateLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception)
            {

                Log.CloseAndFlush();
            }

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
                webBuilder.UseUrls("https://0.0.0.0:5554/");
            });

    }
}
