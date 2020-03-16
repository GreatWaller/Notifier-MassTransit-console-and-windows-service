using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Events.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Notifier
{
    public class Notifier : IConsumer<NotificationEvent>
    {
        readonly ILogger _logger;
        public Notifier(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Notifier>();
        }
        static NotificationController controller = NotificationController.GetInstance();
        public Task Consume(ConsumeContext<NotificationEvent> context)
        {
            _logger.LogInformation("i am from consume");
            if (context.Message.SubscribeID != null)
            {
                controller.NotificationDictionary.GetOrAdd(context.Message.SubscribeID, (p) => new Queue<NotificationEvent>()).Enqueue(context.Message);
            }
            return Task.CompletedTask;
        }
    }
}
