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
        public bool LoadUserProjects(int userid,out List<Project> projects)
        {
            return Load(p => p.Field<int>("USERID") == userid, out projects);
        }
    }
}