using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Arbor.DynamicDns
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Seq("http://localhost:5341")
                .CreateLogger();


            CreateHostBuilder(args, logger).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, ILogger logger) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(services => { services.AddSingleton(logger); })
                .ConfigureAppConfiguration(builder => { builder.AddJsonFile("appsettings.user.json", true); })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}