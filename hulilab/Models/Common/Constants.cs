using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hulilab.Models.Common
{
    public class Constants
    {
        public static readonly string SUCCESSALERT = "<script>alert('保存成功');window.location.assign(\"{0}\");</script>";
        public static readonly string FAILALERT = "<script>alert('{0}');window.history.back();</script>";
        public static readonly string ADMINWEBTITLE = "管理面板";
        public static readonly string WEBSITETITLE = "";
    }

    public enum Language
    {
        ENU,
        CHS,
    }
}