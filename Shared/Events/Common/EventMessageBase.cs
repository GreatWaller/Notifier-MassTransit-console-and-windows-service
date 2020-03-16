using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Events
{
    public class EventMessageBase<TEntity> : EventEntity<TEntity>
    {
        public string DeviceId { get; set; }

        /// <summary>
        /// 订阅资源路径
        /// 资源路径URI(卡口ID、设备ID、采集内容ID、案件ID、目标视图库ID、行政区编号2/4/6位等)支持批量和单个订阅，订阅时必选
        /// </summary>
        public string ResourceURI { get; set; }
    }
}
