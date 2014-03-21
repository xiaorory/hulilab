using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hulilab.Models.DAL;
using hulilab.Models.Repository;
using System.Data;
using hulilab.Models.Common;

namespace hulilab.Models.BLL
{
    public class MemberService : BaseService<Member>, IService<Member>
    {
        public bool LoadCurrentTeachers(out List<Member> currentTeachers)
        {
            return Load(p=>p.Field<bool>("IsTeacher") == true && p.Field<bool>("Status") == true, out currentTeachers);
        }

        public bool LoadCurrentStudents(out List<Member> currentStudents)
        {
            return Load(p => p.Field<bool>("IsTeacher") == false && p.Field<bool>("Status") == true, out currentStudents);
        }

        public bool LoadPastStudents(out List<Member> pastStudents)
        {
            return Load(p => p.Field<bool>("IsTeacher") == false && p.Field<bool>("Status") == false, out pastStudents);
        }

        public static string GetUserName(int? userid,Language lang)
        {
            if (null != userid && userid > 0)
            {
                Member member = new Member();
                member.ID = userid;
                MemberService ms = new MemberService();
                if (ms.Find(member))
                {
                    if (lang == Language.CHS)
                    {
                        return member.ChsName;
                    }
                    else if (lang == Language.ENU)
                    {
                        return member.EnuName;
                    }
                }
            }
            if (lang == Language.CHS)
            {
                return "游客";
            }
            else
            {
                return "Anonymous User";
            }
        }
    }
}