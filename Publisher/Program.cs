using MassTransit;
using Shared.Entities;
using Shared.Events;
using System;
using System.Threading.Tasks;

namespace Publisher
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host("rabbitmq://localhost");
            });

            await bus.StartAsync(); // This is important!
            await Start(bus);
            //await bus.Publish(new Message { Text = "Hi" });
            await bus.StopAsync();
        }

        static async Task Start(IBusControl bus)
        {
            Console.WriteLine("Press 'Enter' to publish the entity created event");
            Console.WriteLine("Press any other key to exit");

            #region PublishLoop

            while (true)
            {
                var key = Console.ReadKey();
                Console.WriteLine();

                var orderReceivedId = Guid.NewGuid();
                if (key.Key == ConsoleKey.Enter)
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        var faceEvent = new FaceEvent
                        {
                            ResourceURI = orderReceivedId.ToString(),
                            Entity = new Face { Id = 1 },
                            DeviceId = "deviceid"
                        };
                        await bus.Publish(faceEvent)
                            .ConfigureAwait(false);
                    }
                    
                    Console.WriteLine($"Published CreateEventMessage Event with ResourceURI {orderReceivedId}.");
                }
                else
                {
                    return;
                }
                #endregion
            }
        }
    }
}
