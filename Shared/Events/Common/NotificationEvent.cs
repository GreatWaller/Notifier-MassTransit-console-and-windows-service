using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events.Common
{
    public class NotificationEvent
    {
        /// <summary>
        /// 订阅标识符
        /// 该订阅标识符，数据共享接口调用时由VIID生成，级联接口调用时不可为空，取消订阅时必选
        /// </summary>
        public string SubscribeID { get; set; }
        /// <summary>
        /// 信息上报间隔时间
        /// </summary>
        public int ReportInterval { get; set; }

        public string ReceiveAddr { get; set; }
        public IEntity Entity { get; set; }
        public string Type { get; set; }
    }
}
