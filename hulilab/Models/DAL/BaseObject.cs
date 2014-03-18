using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace hulilab.Models.DAL
{
    public class BaseObject
    {
        [DefaultValue(null)]
        public int? ID { get; set; }
    }
}