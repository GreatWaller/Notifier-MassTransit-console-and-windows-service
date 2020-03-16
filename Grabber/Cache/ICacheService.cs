using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabber.Cache
{
    public interface ICacheService
    {
        List<Subscribe> GetAllSubscribes();
        List<Device> GetAllDevices();
        Device GetDeviceById(string id);
    }
}
