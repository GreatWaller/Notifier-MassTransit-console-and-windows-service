using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Common
{
    public class Subscribe
    {
        /// <summary>
        /// 订阅标识符
        /// 该订阅标识符，数据共享接口调用时由VIID生成，级联接口调用时不可为空，取消订阅时必选
        /// </summary>
        [StringLength(33)]
        public string SubscribeID { get; set; }

        /// <summary>
        /// 订阅标题
        /// 描述订阅的主题和目标，订阅时必选
        /// </summary>
        [MaxLength(256)]
        public string Title { get; set; }

        /// <summary>
        /// 订阅类别
        /// 订阅时必选，可同时带多个类别（此时是一个string），用英文半角逗号分隔
        /// GA/T 1400.3 B.3.49　订阅类别（SubscribeDetailType）
        /// </summary>
        public string SubscribeDetail { get; set; }

        /// <summary>
        /// 订阅资源路径
        /// 资源路径URI(卡口ID、设备ID、采集内容ID、案件ID、目标视图库ID、行政区编号2/4/6位等)支持批量和单个订阅，订阅时必选
        /// </summary>
        [MaxLength(256)]
        public string ResourceURI { get; set; }

        /// <summary>
        /// 申请人 订阅时必选
        /// </summary>
        [MaxLength(50)]
        public string ApplicantName { get; set; }

        /// <summary>
        /// 申请单位 订阅时必选 
        /// </summary>
        [MaxLength(100)]
        public string ApplicantOrg { get; set; }

        /// <summary>
        /// 开始时间 订阅时必选 
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间 订阅时必选 
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 信息接收地址
        /// 订阅信息接收地址URL如：http://x.x.x.x/receive1 订阅时必选
        /// </summary>
        [MaxLength(256)]
        public string ReceiveAddr { get; set; }

        /// <summary>
        /// 信息上报间隔时间
        /// 单位为秒（s），<=0表示不限制
        /// </summary>
        public int ReportInterval { get; set; }

        /// <summary>
        /// 理由  进行该订阅的理由
        /// </summary>
        [MaxLength(256)]
        public string Reason { get; set; }

        /// <summary>
        /// 操作类型  0：订阅；1：取消订阅
        /// </summary>
        [Required]
        public int OperateType { get; set; }

        /// <summary>
        /// 订阅执行状态
        /// 0：订阅中  1：已取消订阅  2：订阅到期  9：未订阅
        /// 该字段只读
        /// </summary>
        public int SubscribeStatus { get; private set; }

        /// <summary>
        /// 订阅取消单位 仅在取消订阅时使用 
        /// </summary>
        [MaxLength(100)]
        public string SubscribeCancelOrg { get; set; }

        /// <summary>
        /// 订阅取消人 仅在取消订阅时使用
        /// </summary>
        [MaxLength(32)]
        public string SubscribeCancelPerson { get; set; }

        /// <summary>
        /// 取消时间  仅在取消订阅时使用
        /// </summary>
        public DateTime CancelTime { get; set; }

        /// <summary>
        /// 取消原因  仅在取消订阅时使用
        /// </summary>
        [MaxLength(64)]
        public string CancelReason { get; set; }
    }
}
