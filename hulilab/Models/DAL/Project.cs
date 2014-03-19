using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace hulilab.Models.DAL
{
    public class Project : BaseObject
    {
        public int Userid { get; set; }//对应member id
        [Display(Name="研究期限")]
        public string Duration { get; set; }
        [Display(Name="基金项目英文名称")]
        public string EnuTitle { get; set; }
        [Display(Name = "基金项目中文名称")]
        public string ChsTitle { get; set; }
    }
}