using Microsoft.Extensions.Logging;
using Shared.Cache;
using Shared.Common;
using Shared.Entities;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabber.Grabber
{
    public class FaceGrabber : GrabberBase<FaceEvent, Face>
    {
        public FaceGrabber(ICacheService cacheService, ILoggerFactory loggerFactory) : base(cacheService, loggerFactory)
        {
        }

        public override bool IsSubscribed(FaceEvent createEvent, Shared.Common.Subscribe subscribe)
        {
            log.LogInformation("i am from face grabber");
            var device = _cacheService.GetDeviceById(createEvent.DeviceId);
            if (subscribe.ResourceURI.Equals(device.DeviceId)
                || subscribe.ResourceURI.Contains(device.TollgateId)
                || subscribe.ResourceURI.Contains(device.LaneId))
            {
                return true;
            }
            return false;
        }
    }
}
