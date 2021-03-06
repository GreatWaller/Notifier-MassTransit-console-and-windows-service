﻿using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Cache
{
    public class CacheService : ICacheService
    {
        /// <summary>
        /// 这个接口只能存取string及hash。存取时需要序列化或反序列化并进行string与byte[]的转换
        /// </summary>
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public List<Device> GetAllDevices()
        {
            return new List<Device>();
        }

        public List<Subscribe> GetAllSubscribes()
        {
            var subscribe= new Subscribe
            {
                SubscribeID = "subscribeid",
                ResourceURI = "deviceid",
                ReportInterval=2
            };
            var subscribe2 = new Subscribe
            {
                SubscribeID = "subscribeid2",
                ResourceURI = "deviceid2",
                ReportInterval=5
            };
            var subscribe3 = new Subscribe
            {
                SubscribeID = "subscribeid3",
                ResourceURI = "deviceid3",
                ReportInterval = 8
            };
            var list = new List<Subscribe> {subscribe,subscribe2,subscribe3};
            //var options = new DistributedCacheEntryOptions()
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(600));
            //_cache.Set("test", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(list)), options);
            var data = _cache.Get("test");
            if (data!=null)
            {
                list = JsonConvert.DeserializeObject<List<Subscribe>>(ConvertFromByteArray(data));
            }
            return list;
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

        private byte[] ConvertToByteArray(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }
        private string ConvertFromByteArray(byte[] ba)
        {
            return Encoding.UTF8.GetString(ba);
        }
    }
}
