using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hulilab.Models.Common
{
    public class Constants
    {
        public static readonly string SUCCESSALERT = "<script>alert('操作成功');window.location.assign(\"{0}\");</script>";
        public static readonly string FAILALERT = "<script>alert('{0}');window.history.back();</script>";
        public static readonly string ADMINWEBTITLE = "管理面板";
        public static readonly string WEBSITETITLE = "";
    }

    public enum Language
    {
        ENU,
        CHS,
    }

    //1为海报,2为资料总结,3为软件，0为未分类
    public enum ShareType
    {
        UNDEFINED = 0,//未分类
        POSTER=1,//海报
        SUMMARY=2,//资料总结
        SOFTWARE=3,//软件
    }
}