using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;

namespace hulilab.Models.DAL
{
    public class Member :BaseObject
    {
        public string Username{get;set;}
        public string Password { get; set; }
        public string Name { get; set; }
        public string ChsName{get;set;}
        public StringBuilder Introduction{get;set;}
        public StringBuilder ChsIntroduction{get;set;}
        public string ResearchField{get;set;}
        public string ChsResearchField{get;set;}
        public string Phone{get;set;}
        public string Email{get;set;}
        [DefaultValue(null)]
        public bool? Status{get;set;}//以前的学生为false,现在的学生为true
        [DefaultValue(null)]
        public bool? IsTeacher { get; set; } //老师为true,学生为false
    }
}