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
        /// <summary>
        /// 检查当前用户是否是是实验室成员
        /// </summary>
        /// <returns>如果没有登录，返回null。否则返回member实例</returns>
        private Member CheckMembership()
        {
            int? userId = StringHelper.ConvertObjectToInt(Request.Cookies["userId"].Value);

            if (null != userId && userId > 0)
            {
                Member member = new Member();
                member.ID = userId;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    return member;
                }
            }
            return null;
        }
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
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
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 新增成员页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddMember()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 更改成员信息页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditMember()
        {
            Member user = CheckMembership();
            if (user != null)
            {
                int? userId = StringHelper.ConvertObjectToInt(Request.Params["userid"]);
                if (null == userId)
                {
                    userId = StringHelper.ConvertObjectToInt(Session["userId"]);
                    return View(user);
                }
                else
                {
                    Member member = new Member();
                    member.ID = userId;
                    MemberService ms = new MemberService();
                    if (ms.Find(member))
                    {
                        return View(member);
                    }
                    else
                    {
                        return Content(string.Format(Constants.FAILALERT, "没有找到该用户！"));
                    }
                }
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 基金管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProjectManagement()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 新增基金页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProject()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 修改基金页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProject()
        {
            if (CheckMembership() != null)
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
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 删除一个基金项目
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteProject()
        {
            if (CheckMembership() != null)
            {
                int? projectId = StringHelper.ConvertObjectToInt(Request.Params["projectId"]);
                int? userId = StringHelper.ConvertObjectToInt(Request.Params["userId"]);

                if (null != projectId && projectId > 0 && null != userId && userId > 0)
                {
                    Project project = new Project();
                    project.ID = projectId;
                    project.Userid = (int)userId;
                    ProjectService ps = new ProjectService();
                    if (ps.Delete(project))
                    {
                        return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/EditMember?userid=" + project.Userid)));
                    }
                }
                return Content(string.Format(Constants.FAILALERT, "没有找到该基金。"));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 共享资料管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ShareManagement()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 新增资料分享页面
        /// </summary>
        /// <returns></returns>
        public ActionResult AddShare()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 编辑资料分享页面
        /// </summary>
        /// <returns></returns>
        public ActionResult EditShare()
        {
            if (CheckMembership() != null)
            {
                int? shareId = StringHelper.ConvertObjectToInt(Request.Params["shareId"]);

                if (null != shareId && shareId > 0)
                {
                    Share share = new Share();
                    share.ID = shareId;
                    ShareService ss = new ShareService();
                    if (ss.Find(share))
                    {
                        return View(share);
                    }
                }
                return Content(string.Format(Constants.FAILALERT, "没有找到该共享资料。"));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 网站管理
        /// </summary>
        /// <returns></returns>
        public ActionResult SiteManagement()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 修改网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult EditWelcomePage()
        {
            if (CheckMembership() != null)
            {
                TextSettingsService tss = new TextSettingsService();
                TextSettings ts;
                if (tss.First(out ts))
                {
                    ts.Content = HttpUtility.HtmlDecode(ts.Content);
                    return View(ts);
                }
                else 
                {
                    ts = new TextSettings();
                    ts.Content = " ";
                    if (tss.Add(ts))
                    {
                        return View(ts);
                    }
                }
                return Content(string.Format(Constants.FAILALERT, tss.ErrorMsg));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 评论管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CommentManagement()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 论文管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult PublicationManagement()
        {
            Member user = CheckMembership();
            if (null != user && null != user.IsTeacher && (bool)user.IsTeacher)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面(仅实验室老师有权限访问此页面)，请先登录！"));
            }
        }

        /// <summary>
        /// 合作伙伴管理页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CollaborationManagement()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 新增合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult AddCollaboration()
        {
            if (CheckMembership() != null)
            {
                return View();
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 修改合作伙伴信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EditCollaboration()
        {
            if (CheckMembership() != null)
            {
                int? collaborationId = StringHelper.ConvertObjectToInt(Request.Params["collaborationId"]);

                if (null != collaborationId && collaborationId > 0)
                {
                    Collaboration collaboration = new Collaboration();
                    collaboration.ID = collaborationId;
                    CollaborationService cs = new CollaborationService();
                    if (cs.Find(collaboration))
                    {
                        return View(collaboration);
                    }
                }
                return Content(string.Format(Constants.FAILALERT, "没有找到该合作伙伴。"));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

        /// <summary>
        /// 删除一个合作伙伴
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteCollaboration()
        {
            if (CheckMembership() != null)
            {
                int? collaborationId = StringHelper.ConvertObjectToInt(Request.Params["collaborationId"]);

                if (null != collaborationId && collaborationId > 0)
                {
                    Collaboration collaboration = new Collaboration();
                    collaboration.ID = collaborationId;
                    CollaborationService cs = new CollaborationService();
                    if (cs.Delete(collaboration))
                    {
                        return Content(string.Format(Constants.SUCCESSALERT, Url.Content("~/Admin/CollaborationManagement")));
                    }
                }
                return Content(string.Format(Constants.FAILALERT, "没有找到该合作伙伴。"));
            }
            else
            {
                return Content(string.Format(Constants.FAILALERT, "你没有权限访问此页面，请先登录！"));
            }
        }

    }
}
