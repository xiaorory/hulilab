using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using hulilab.Models.Common;

namespace hulilab.Models.DAL
{
    public class Publication : BaseObject
    {
        [Required]
        [DisplayName("标题")]
        public string Title { get; set; }
        [Required]
        [DisplayName("链接")]
        public string Link { get; set; }
        [Required]
        [DisplayName("作者列表")]
        public string Authors { get; set; }
        [DisplayName("发表杂志")]
        public string Magazine { get; set; }
        [DisplayName("发表时间")]
        public string PublishYear { get; set; }
        [DefaultValue(null)]
        [DisplayName("引用次数")]
        public int? CitedBy { get; set; }
        /// <summary>
        /// 获取影响因子
        /// </summary>
        public string GetIF()
        {
            return StringHelper.GetIF(Magazine);
        }
    }
}