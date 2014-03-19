using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using hulilab.Models.DAL;
using hulilab.Models.BLL;

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
        /// 个人资料编辑页面
        /// </summary>
        /// <returns></returns>
        public ActionResult MemberManagement()
        {
            return View();
        }

        /// <summary>
        /// 新增成员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMember()
        {
            return View();
        }

        /// <summary>
        /// 新增基金页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProject()
        {
            return View();
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

    }
}
