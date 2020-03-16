using Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Shared.Common
{
    public class Notification
    {
        /// <summary>
        /// 通知标识符 订阅的通知标识符
        /// </summary>
        [Required]
        [StringLength(33)]
        public string NotificationID { get; set; }

        /// <summary>
        /// 订阅标识符
        /// 该订阅标识符，数据共享接口调用时由VIID生成，级联接口调用时不可为空，取消订阅时必选
        /// </summary>
        [Required]
        [StringLength(33)]
        public string SubscribeID { get; set; }

        /// <summary>
        /// 订阅标题
        /// 描述订阅的主题和目标，订阅时必选
        /// </summary>
        [Required]
        [MaxLength(256)]
        public string Title { get; set; }

        /// <summary>
        /// 触发时间  
        /// </summary>
        [Required]
        public DateTime TriggerTime { get; set; }

        /// <summary>
        /// 信息标识
        /// 订阅通知的详细信息(人、车、物、场景)标识集合
        /// </summary>
        [MaxLength(1024)]
        public string InfoIDs { get; set; }

        public List<Face> FaceObjectList { get; set; }
        public List<Person> PersonObjectList { get; set; }

        public Notification()
        {
            FaceObjectList = new List<Face>();
            PersonObjectList = new List<Person>();
        }
    }
}
