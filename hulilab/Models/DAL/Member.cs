using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace hulilab.Models.DAL
{
    public class Member :BaseObject
    {
        [Required]
        [DisplayName("用户名")]
        public string Username{get;set;}
        [Required]
        [DisplayName("密码")]
        public string Password { get; set; }
        [Required]
        [DisplayName("英文名")]
        public string EnuName { get; set; }
        [Required]
        [DisplayName("中文名")]
        public string ChsName{get;set;}
        [Required]
        [Description("0为女性，1为男性")]
        [DefaultValue(null)]
        [DisplayName("性别")]
        public bool? Sex { get; set; } //0为女性，1为男性
        [DisplayName("教育背景")]
        public StringBuilder EnuEducationHistory { get; set; }
        [DisplayName("教育背景")]
        public StringBuilder ChsEducationHistory { get; set; }
        [DisplayName("获奖情况")]
        public StringBuilder EnuAwards { get; set; }
        [DisplayName("获奖情况")]
        public StringBuilder ChsAwards { get; set; }
        [DisplayName("自我介绍")]
        public StringBuilder EnuIntroduction{get;set;}
        [DisplayName("自我介绍")]
        public StringBuilder ChsIntroduction{get;set;}
        [DisplayName("研究领域")]
        public string EnuResearchField{get;set;}
        [DisplayName("研究领域")]
        public string ChsResearchField{get;set;}
        [DisplayName("学术职位")]
        public string EnuAcademicPosition { get; set; }
        [DisplayName("学术职位")]
        public string ChsAcademicPosition { get; set; }
        [DisplayName("手机")]
        public string Phone{get;set;}
        [DisplayName("邮箱")]
        public string Email{get;set;}
        [Required]
        [DefaultValue(null)]
        [DisplayName("是否是老师")]
        public bool? Status{get;set;}//以前的学生为false,现在的学生为true
        [Required]
        [DefaultValue(null)]
        [DisplayName("是否是现任成员")]
        public bool? IsTeacher { get; set; } //老师为true,学生为false
    }
}