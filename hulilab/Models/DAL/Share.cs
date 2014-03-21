using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace hulilab.Models.DAL
{
    public class Share:BaseObject
    {
        [Required]
        public int Author { get; set; }//对应member id
        [Required]
        [DisplayName("标题")]
        public string Title { get; set; }
        [DisplayName("概括")]
        public string Description { get; set; }
        [DisplayName("内容")]
        public string Content { get; set; }
        [DisplayName("文件类型")]
        [DefaultValue(null)]
        public int? Type { get; set; } //0为poster,1为资料总结,null为未填写
    }
}