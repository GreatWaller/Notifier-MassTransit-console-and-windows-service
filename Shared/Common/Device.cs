using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Common
{
    public class Device
    {
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceId { get; set; }
        public string LaneId { get; set; }
        public string TollgateId { get; set; }
        public int IsOnline { get; set; }

    }
}
