using Grabber.Cache;
using Grabber.Grabber;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Shared.Entities;
using Shared.Events;
using System;
using System.Threading.Tasks;

namespace Grabber
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure => configure.AddConsole());
            serviceCollection.AddTransient<ICacheService, CacheService>();

            serviceCollection.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<FaceGrabber>();
                cfg.AddConsumer<PersonGrabber>();
                cfg.AddBus(ConfigureBus);
            });

            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            var bus = serviceProvider.GetService<IBusControl>();
            await bus.StartAsync(); // This is important!
            Console.WriteLine("Press any key to exit");
            await Task.Run(() => Console.ReadKey());

            await bus.StopAsync();
        }

        private static IBusControl ConfigureBus(IServiceProvider provider)
        {
            return Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");

                //strange,it doesn't work
                //sbc.ReceiveEndpoint("grabber_queue", ep =>
                //{
                //    ep.PrefetchCount = 16;
                //});
                sbc.ConfigureEndpoints(provider);
            });
        }
    }
}
