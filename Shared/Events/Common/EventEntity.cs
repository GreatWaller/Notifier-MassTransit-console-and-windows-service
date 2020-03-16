using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public class EventEntity<TEntity>
    {
        public TEntity Entity { get; set; }
    }
}
