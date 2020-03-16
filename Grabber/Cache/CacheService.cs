using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabber.Cache
{
    public class CacheService : ICacheService
    {
        public List<Device> GetAllDevices()
        {
            return new List<Device>();
        }

        public List<Subscribe> GetAllSubscribes()
        {
            return new List<Subscribe> {
                new Subscribe
                {
                    SubscribeID="subscribeid",
                    ResourceURI="deviceid"
                }};
        }

        public Device GetDeviceById(string id)
        {
            return new Device
            {
                DeviceId = "deviceid",
                TollgateId = "tollgateid",
                LaneId = "laneid"
            };
        }
    }
}
