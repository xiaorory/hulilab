using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using hulilab.Models.DAL;

namespace hulilab.Models.BLL
{
    public class ShareService : BaseService<Share>, IService<Share>
    {
        /// <summary>
        /// 获取某个用户的共享资料
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="shares"></param>
        /// <returns></returns>
        public bool LoadUserShares(int userid, out List<Share> shares)
        {
            return Load(p => p.Field<int>("Author") == userid, out shares);
        }

        /// <summary>
        /// 获取某个类型的共享资料
        /// </summary>
        /// <param name="type">1为海报,2为资料总结,3为软件，0为未填写</param>
        /// <param name="shares"></param>
        /// <returns></returns>
        public bool LoadCertainTypeProjects(int type, out List<Share> shares)
        {
            return Load(p => p.Field<int>("Type") == type, out shares);
        }
    }
}