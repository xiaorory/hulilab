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

        public static string GetSex(bool? sex, Language lang)
        {
            string result = null;
            if (null == sex)
            {
                if (lang == Language.ENU)
                {
                    result = "Secret";
                }
                else if (lang == Language.CHS)
                {
                    result = "保密";
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

        public static string GetShareType(int? type, Language lang)
        {
            string result = null;
            if (null != type)
            {
                if (lang == Language.ENU)
                {
                    switch ((int)type)
                    {
                        case (int)ShareType.POSTER:
                            result = "Poster";
                            break;
                        case (int)ShareType.SUMMARY:
                            result = "Summary";
                            break;
                        case (int)ShareType.SOFTWARE:
                            result = "Software";
                            break;
                        default:
                            result = "Undefined";
                            break;
                    }
                }
                else if (lang == Language.CHS)
                {
                    switch ((int)type)
                    {
                        case (int)ShareType.POSTER:
                            result = "海报";
                            break;
                        case (int)ShareType.SUMMARY:
                            result = "资料总结";
                            break;
                        case (int)ShareType.SOFTWARE:
                            result = "软件";
                            break;
                        default:
                            result = "未分类";
                            break;
                    }
                }
            }
            return result;
        }

        public static string GetStudentType(Int16? type, Language lang)
        {
            string result = null;
            if (null != type)
            {
                if (lang == Language.ENU)
                {
                    switch ((int)type)
                    {
                        case (int)StudentType.PHDCANDIDATE:
                            result = "Ph.d candidate";
                            break;
                        case (int)StudentType.POSTGRADUATE:
                            result = "Master candidate";
                            break;
                        case (int)StudentType.UNDERGRADUATE:
                            result = "Undergraduate";
                            break;
                        default:
                            result = "Undefined";
                            break;
                    }
                }
                else if (lang == Language.CHS)
                {
                    switch ((int)type)
                    {
                        case (int)StudentType.PHDCANDIDATE:
                            result = "博士";
                            break;
                        case (int)StudentType.POSTGRADUATE:
                            result = "硕士";
                            break;
                        case (int)StudentType.UNDERGRADUATE:
                            result = "本科";
                            break;
                        default:
                            result = "未分类";
                            break;
                    }
                }
            }
            return result;
        }


        public static string GetIF(string magazine)
        {
            string result = null;
            if (!string.IsNullOrEmpty(magazine))
            {
                magazine = magazine.ToLower();
                if (magazine.Contains("neuroimage"))
                {
                    result = "6.252";
                }
                else if (magazine.Contains("plos one"))
                {
                    result = "3.73";
                }
                else if (magazine.Contains("journal of pain"))
                {
                    result = "3.240";
                }
                else if (magazine.Contains("psychophysiology"))
                {
                    result = "3.261";
                }
                else if (magazine.StartsWith("nature communication"))
                {
                    result = "10.02";
                }
                else if (magazine.StartsWith("pain"))
                {
                    result = "5.644";
                }
            }
            return result;
        }

        public static String convertStringLineBreakToHtmlFormat(String content)
        {
            if (null != content)
            {
                return content.Replace("\r\n", "<br/>").Replace("\n", "<br/>");
            }
            else
            {
                return "";
            }
        }
    }
}