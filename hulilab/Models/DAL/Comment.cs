using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Text;

namespace hulilab.Models.DAL
{
    public class Comment : BaseObject
    {
        [DefaultValue(null)]
        public int? ShareId { get; set; }// 对应 share id,null代表对网站评论
        [DefaultValue(null)]
        public int? Author { get; set; } //对应member id， null代表匿名
        public string Content { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}