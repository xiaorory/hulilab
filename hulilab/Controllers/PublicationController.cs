using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hulilab.Models.BLL;
using hulilab.Models.DAL;
using hulilab.Models.Common;

namespace hulilab.Controllers
{
    public class PublicationController : Controller
    {
        //
        // GET: /Publication/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Update()
        {
            PublicationService ps = new PublicationService();
            if (ps.Clear(new Publication()) && ps.UpdatePublicationDatabase())
            {
                return Content(String.Format(Constants.SUCCESSALERT,Url.Content("~/Admin/PublicationManagement")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, ps.ErrorMsg));
            }

        }
    }
}
