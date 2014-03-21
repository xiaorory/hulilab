using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using hulilab.Models.DAL;

namespace hulilab.Models.BLL
{
    public class CommentService : BaseService<Comment>,IService<Comment>
    {
        /// <summary>
        /// 获取某个共享资料的评论
        /// </summary>
        /// <param name="shareId"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool LoadShareComments(int shareId, out List<Comment> comments)
        {
            return Load(p => p.Field<int>("ShareId") == shareId, out comments);
        }

        /// <summary>
        /// 获取某个用户所做的评论
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool LoadUserComments(int userid, out List<Comment> comments)
        {
            return Load(p => p.Field<int>("Author") == userid, out comments);
        }

        /// <summary>
        /// 获取某个用户共享资料的评论
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="comments"></param>
        /// <returns></returns>
        public bool LoadUserShareComments(int userid, out List<Comment> comments)
        {
            comments = new List<Comment>();
            ShareService ss = new ShareService();
            List<Share> shares;
            List<int> shareIds = new List<int>();
            bool isSuccess = ss.LoadUserShares(userid, out shares);
            if (isSuccess)
            {
                foreach (Share share in shares)
                {
                    List<Comment> temp;
                    isSuccess = LoadShareComments((int)share.ID, out temp) && isSuccess;
                    if (isSuccess)
                    {
                        comments.AddRange(temp);
                    }
                }
            }
            return isSuccess;
        }
    }
}