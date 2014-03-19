using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hulilab.Models.Common
{
    public class StringHelper
    {
        public static int? ConvertObjectToInt(object obj)
        {
            int target;
            if (null != obj && int.TryParse(obj.ToString(), out target))
            {
                return target;
            }
            else
            {
                return null;
            }
             
        }

        public static string GetSex(bool? sex,Language lang)
        {
            string result= null;
            if (null == sex)
            {
                if (lang == Language.ENU)
                {
                    result = "Not Filled";
                }
                else if(lang == Language.CHS)
                {
                    result = "主人很懒，没有填写";
                }
            }
            else
            {
                if (lang == Language.ENU)
                {
                    result = (bool)sex ? "Male" : "Female";//0为女性，1为男性
                }
                else if (lang == Language.CHS)
                {
                    result = (bool)sex ? "男" : "女";//0为女性，1为男性
                }
            }
            return result;
        }
    }
}