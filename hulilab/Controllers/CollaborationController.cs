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
    public class CollaborationController : Controller
    {
        //
        // GET: /Collaboration/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 增加合作伙伴
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCollaboration(Collaboration collaboration)
        {
            CollaborationService cs = new CollaborationService();
            if (cs.Add(collaboration))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/CollaborationManagement")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, cs.ErrorMsg));
            }
        }

        /// <summary>
        /// 修改合作伙伴信息
        /// </summary>
        /// <param name="collaboration"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditCollaboration(Collaboration collaboration)
        {
            CollaborationService cs = new CollaborationService();
            if (cs.Edit(collaboration))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditCollaboration?collaborationId=" + collaboration.ID)));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, cs.ErrorMsg));
            }
        }
    }
}
