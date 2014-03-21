using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using hulilab.Models.DAL;
using hulilab.Models.BLL;
using hulilab.Models.Common;

namespace hulilab.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 成员管理界面
        /// </summary>
        /// <returns></returns>
        public ActionResult TeamManagement()
        {
            return View();
        }

        /// <summary>
        /// 新增成员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMember()
        {
            int? userId = StringHelper.ConvertObjectToInt(Session["userId"]);
           
            if (null != userId && userId > 0)
            {
                Member member = new Member();
                member.ID = userId;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 更改成员信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMember()
        {
            int? userId = StringHelper.ConvertObjectToInt(Request.Params["userid"]);
            Member member = new Member();

            if (null == userId)
            {
                userId = StringHelper.ConvertObjectToInt(Session["userId"]);
            }

            if (null != userId && userId > 0)
            {
                member.ID = userId;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    return View(member);
                }
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 基金管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectManagement()
        {
            int? userId = StringHelper.ConvertObjectToInt(Request.Params["userid"]);

            if (null == userId)
            {
                userId = StringHelper.ConvertObjectToInt(Session["userId"]);
            }

            if (null != userId && userId > 0)
            {
                Member member = new Member();
                member.ID = userId;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 新增基金页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProject()
        {
            int? userId = StringHelper.ConvertObjectToInt(Request.Params["userid"]);

            if (null == userId)
            {
                userId = StringHelper.ConvertObjectToInt(Session["userId"]);
            }

            if (null != userId && userId > 0)
            {
                Member member = new Member();
                member.ID = userId;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    return View();
                }
            }
            return Content(string.Format(Constants.FAILALERT, "没有权限。"));
        }

        /// <summary>
        /// 修改基金页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            int? projectId = StringHelper.ConvertObjectToInt(Request.Params["projectId"]);
            int? userId = StringHelper.ConvertObjectToInt(Request.Params["userId"]);

            if (null != projectId && projectId > 0 && null != userId && userId > 0)
            {
                Project project = new Project();
                project.ID = projectId;
                project.Userid = (int)userId;
                ProjectService ps = new ProjectService();
                if (ps.Find(project))
                {
                    return View(project);
                }
            }
            return Content(string.Format(Constants.FAILALERT, "没有找到该基金。"));
        }

        /// <summary>
        /// 共享资料管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShareManagement()
        {
            return View();
        }

        /// <summary>
        /// 评论管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentManagement()
        {
            return View();
        }

        public ActionResult PublicationManagement()
        {
            return View();
        }
    }
}
