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
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 增加评论
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        public ActionResult AddComment(Comment comment)
        {
            comment.SubmitDate = DateTime.Now;
            CommentService cs = new CommentService();
            if (cs.Add(comment))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Share?shareId="+comment.ShareId)));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, cs.ErrorMsg));
            }
        }
    }
}
