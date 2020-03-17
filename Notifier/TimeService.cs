using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Shared.Cache;
using Shared.Common;
using Shared.Entities;
using Shared.Events.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Notifier
{
    public class TimeService: IHostedService
    {
        private readonly ILogger _logger;
        private readonly ICacheService _cacheService;
        private readonly IDistributedCache _cache;
        private readonly IBusControl _bus;
        NotificationController controller = NotificationController.GetInstance();
        Timer _timer;
        TimeNode[] timeCircle;
        int location;

        public TimeService(ILoggerFactory loggerFactory, 
            ICacheService cacheService,
            IDistributedCache cache,
            IBusControl bus)
        {
            _logger = loggerFactory.CreateLogger<TimeService>();
            _cacheService = cacheService;
            _bus = bus;
            _cache = cache;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            InitTimeCircle();
            _logger.LogInformation("Starting bus");
            await _bus.StartAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping bus");
            return _bus.StopAsync(cancellationToken);
        }


        private void InitTimeCircle()
        {
            //get all subscribes, 以确定数组长度：单位时间为最小公约数，长度为最小公倍数/单位时间
            var subscribes = _cacheService.GetAllSubscribes();

            var count = 2;
            timeCircle = new TimeNode[count];
            //
            for (int i = 0; i < count; i++)
            {
                timeCircle[i] = new TimeNode();
                timeCircle[i].Subscribes.Add("subscribeid");
            }

            _timer = new Timer(Tick, null, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(5));
        }

        //拨动时间轮
        private void Tick(object source)
        {
            location += 1;
            location = location % 2;
            HandleNode(timeCircle[location]);
        }

        //当时间拨动到某个TimeNode上时，依次处理该时间点需要发送的订阅
        private void HandleNode(TimeNode node)
        {
            foreach (var subscribe in node.Subscribes)
            {
                var notification = PrepareNotificationBySubscribeId(subscribe);
                DoPost(notification);
            }
        }
        // 准备每个订阅：从字典中去取
        private Notification PrepareNotificationBySubscribeId(string subscribeId)
        {
            Queue<NotificationEvent> notificationEventQueue;
            if (!controller.NotificationDictionary.TryGetValue(subscribeId, out notificationEventQueue))
            {
                return null;
            }
            var count = notificationEventQueue.Count;
            if (count == 0)
            {
                return null;
            }
            var notification = new Notification { NotificationID = "notificationid", SubscribeID = subscribeId };
            //依据类型存入对应字段
            for (int i = 0; i < count; i++)
            {
                var message = notificationEventQueue.Dequeue();
                switch (message.Type?.ToLower())
                {
                    case "face":
                        notification.FaceObjectList.Add(message.Entity as Face);
                        break;
                    case "person":
                        notification.PersonObjectList.Add(message.Entity as Person);
                        break;
                    default:
                        break;
                }

            }
            return notification;
        }


        private void DoPost(Notification notification)
        {
            if (notification != null)
            {
                _logger.LogInformation($"do {notification.FaceObjectList.Count} post task");
            }
        }
    }

    public class TimeNode
    {
        public List<string> Subscribes { get; set; } = new List<string>();
    }
}
