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
    public class TextSettingsController : Controller
    {
        /// <summary>
        /// 修改首页欢迎语
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWelcomePage(TextSettings ts)
        {
            TextSettingsService tss = new TextSettingsService();
            ts.Content = HttpUtility.HtmlEncode(ts.Content);
            if (tss.Edit(ts))
            {
                return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditWelcomePage")));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, tss.ErrorMsg));
            }
        }

    }
}
