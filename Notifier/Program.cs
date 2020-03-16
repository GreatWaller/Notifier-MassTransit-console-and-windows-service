using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Notifier
{
    class Program
    {
        public static AppConfig AppConfig { get; set; }
        static async Task Main(string[] args)
        {
            var isService = !(Debugger.IsAttached || args.Contains("--console"));

            var builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true);
                    config.AddEnvironmentVariables();

                    if (args != null)
                        config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<AppConfig>(hostContext.Configuration.GetSection("AppConfig"));

                    services.AddMassTransit(cfg =>
                    {
                        cfg.AddConsumer<Notifier>();
                        cfg.AddBus(ConfigureBus);
                        //cfg.AddRequestClient<IsItTime>();
                    });

                    services.AddHostedService<MassTransitConsoleHostedService>();
                    //services.AddHostedService<CheckTheTimeService>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                });

            if (isService)
            {
                await builder.UseWindowsService().Build().RunAsync();
                //await builder.UseSystemd().Build().RunAsync(); // For Linux, replace the nuget package: "Microsoft.Extensions.Hosting.WindowsServices" with "Microsoft.Extensions.Hosting.Systemd", and then use this line instead
            }
            else
            {
                await builder.RunConsoleAsync();
            }

        }

        static IBusControl ConfigureBus(IServiceProvider provider)
        {
            AppConfig = provider.GetRequiredService<IOptions<AppConfig>>().Value;
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(AppConfig.Host, AppConfig.VirtualHost, h =>
                {
                    h.Username(AppConfig.Username);
                    h.Password(AppConfig.Password);
                    
                });

                cfg.ConfigureEndpoints(provider);
            });
        }
    }
}
