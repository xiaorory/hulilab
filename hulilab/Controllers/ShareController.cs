using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hulilab.Models.DAL;
using hulilab.Models.BLL;
using hulilab.Models.Common;

namespace hulilab.Controllers
{
    public class ShareController : Controller
    {
        //
        // GET: /Share/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 查看某一类型的所有共享资料
        /// </summary>
        /// <returns></returns>
        public ActionResult Shares()
        {
            return View();
        }

        /// <summary>
        /// 增加新的资料分享
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult AddShare(Share share)
        {
            ShareService ss = new ShareService();
            if (ss.Add(share))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/ShareManagement")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ss.ErrorMsg));
            }
        }

        /// <summary>
        /// 编辑分享资料信息
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult EditShare(Share share)
        {
            ShareService ss = new ShareService();
            if (ss.Edit(share))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditShare?shareId="+share.ID)));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ss.ErrorMsg));
            }
        }
    }
}
