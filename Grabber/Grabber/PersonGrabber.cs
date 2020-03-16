using Grabber.Cache;
using Microsoft.Extensions.Logging;
using Shared.Common;
using Shared.Entities;
using Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grabber.Grabber
{
    public class PersonGrabber : GrabberBase<PersonEvent, Person>
    {
        public PersonGrabber(ICacheService cacheService, ILoggerFactory loggerFactory) : base(cacheService, loggerFactory)
        {
        }

        public override bool IsSubscribed(PersonEvent createEvent, Subscribe subscribe)
        {
            throw new NotImplementedException();
        }
    }
}
