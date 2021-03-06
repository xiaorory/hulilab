﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hulilab.Models.DAL;
using hulilab.Models.BLL;
using hulilab.Models.Common;

namespace hulilab.Controllers
{
    public class MemberController : Controller
    {
        //
        // GET: /Member/
        //个人详细资料页面
        public ActionResult Index()
        {
            return View();
        }

        //组内成员展示
        public ActionResult Team()
        {
            return View();
        }

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckLogIn()
        {
            string username = Request.Params["username"].ToLower();
            string password = Request.Params["password"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                Member member = new Member();
                member.Username = username;
                member.Password = password;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    Response.Cookies["userId"].Value = member.ID.ToString();
                    if (null != member.IsTeacher && (bool)member.IsTeacher)
                    {
                        Response.Cookies["isTeacher"].Value = bool.TrueString;
                    }
                    else
                    {
                        Response.Cookies["isTeacher"].Value = bool.FalseString;
                    }
                    
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return Content(string.Format(Constants.FAILALERT, "登录名或者密码不正确"));
                }
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "登录名或者密码为空"));
            }
        }

        /// <summary>
        /// 增加新成员
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddMember(Member member)
        {
            MemberService ms = new MemberService();
            if (ms.Add(member))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/TeamManagement")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ms.ErrorMsg));
            }
        }

        /// <summary>
        /// 修改成员信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditMember(Member member)
        {
            MemberService ms = new MemberService();
            if (ms.Edit(member))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditMember?userid=" + member.ID)));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ms.ErrorMsg));
            }
        }

        /// <summary>
        /// 增加成员基金
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddProject(Project project)
        {
            ProjectService ps = new ProjectService();
            if (ps.Add(project))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/AddProject")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ps.ErrorMsg));
            }
        }

        /// <summary>
        /// 修改成员基金
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditProject(Project project)
        {
            ProjectService ps = new ProjectService();
            if (ps.Edit(project))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditProject?projectid=" + project.ID + "&userid=" + project.Userid)));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ps.ErrorMsg));
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
