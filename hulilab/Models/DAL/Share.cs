using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace hulilab.Models.DAL
{
    public class Share:BaseObject
    {
        public int Author { get; set; }//对应member id
        public string Title { get; set; }
        public string Description { get; set; }
        public StringBuilder Content { get; set; }
    }
}