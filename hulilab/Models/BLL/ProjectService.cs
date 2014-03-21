using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hulilab.Models.DAL;
using System.Data;

namespace hulilab.Models.BLL
{
    public class ProjectService: BaseService<Project>,IService<Project>
    {
        /// <summary>
        /// 获取某个用户所拥有的基金项目
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        public bool LoadUserProjects(int userid,out List<Project> projects)
        {
            return Load(p => p.Field<int>("USERID") == userid, out projects);
        }
    }
}