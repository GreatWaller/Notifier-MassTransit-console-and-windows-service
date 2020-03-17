using Microsoft.Extensions.Logging;
using Shared.Common;
using Shared.Entities;
using Shared.Events.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Notifier
{
    public class NotificationController
    {
        //private static ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
        //{
        //    builder
        //        .AddFilter("Microsoft", LogLevel.Warning)
        //        .AddFilter("System", LogLevel.Warning)
        //        .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
        //        .AddConsole()
        //        .AddEventLog();
        //});
        //private static ILogger logger = loggerFactory.CreateLogger<NotificationController>();
        private static NotificationController _notificationController = null;
        /// <summary>
        /// key: subscribeId;
        /// </summary>
        public ConcurrentDictionary<string, ConcurrentQueue<NotificationEvent>> NotificationDictionary { get; set; }
            = new ConcurrentDictionary<string, ConcurrentQueue<NotificationEvent>>();
        public int Limatation { get; set; } = 1;

        static NotificationController()
        {
            _notificationController = new NotificationController();
        }
        public static NotificationController GetInstance()
        {
            return _notificationController;
        }

        //启动定时器：初始化TimeCircle及挂载每个TimeNode中的Subscribe
        //节点数量=最小公位数/最小间隔
        //public void StartTimer()
        //{
        //    //初始化time circle
        //    InitTimeCircle();

        //    timer.Elapsed += new ElapsedEventHandler(Tick);
        //    timer.Enabled = true;
        //    timer.Interval = 5000;
        //    timer.AutoReset = true;

        //}

    }
}
