using Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabber.Grabber
{
    public interface IGrabber<TEvent>
    {
        List<Subscribe> GetALLSubscribes(TEvent createEvent);
    }
}
