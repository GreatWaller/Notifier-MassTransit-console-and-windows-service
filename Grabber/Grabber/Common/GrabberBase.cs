using Grabber.Cache;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Common;
using Shared.Entities;
using Shared.Events;
using Shared.Events.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Grabber.Grabber
{
    public abstract class GrabberBase<TEvent, TEntity> : IGrabber<TEvent>,
        IConsumer<TEvent>
        where TEvent : EventMessageBase<TEntity>
        where TEntity : IEntity
    {
        //static protected ILog log = LogManager.GetLogger<GrabberBase<TEvent, TEntity>>();
        protected readonly ICacheService _cacheService;
        protected readonly ILogger<GrabberBase<TEvent, TEntity>> log;
        public GrabberBase(ICacheService cacheService, ILoggerFactory loggerFactory)
        {
            _cacheService = cacheService;
            this.log = loggerFactory.CreateLogger<GrabberBase<TEvent, TEntity>>();
        }

        public List<Subscribe> GetALLSubscribes(TEvent createEvent)
        {
            //todo:规则梳理,暂时仅通过多数类型都涉及到的 设备号来决定
            var subscribes = _cacheService.GetAllSubscribes();
            var result = new List<Subscribe>();
            foreach (var subscribe in subscribes)
            {
                if (IsSubscribed(createEvent, subscribe))
                {
                    result.Add(subscribe);
                }
            }
            return result;
        }

        public abstract bool IsSubscribed(TEvent createEvent, Subscribe subscribe);
        public async Task Consume(ConsumeContext<TEvent> context)
        {
            log.LogInformation($"Subscriber has received CreateEventMessage event with URI {context.Message.ResourceURI}.");

            var subscribes = GetALLSubscribes(context.Message);

            foreach (var subscribe in subscribes)
            {
                var response = new NotificationEvent
                {
                    Entity = context.Message.Entity,
                    Type = typeof(TEntity).Name.ToString(),
                    SubscribeID = subscribe.SubscribeID,
                    ReportInterval = subscribe.ReportInterval,
                    ReceiveAddr = subscribe.ReceiveAddr
                };

                await context.Publish(response).ConfigureAwait(false);
            }
        }
    }
}
