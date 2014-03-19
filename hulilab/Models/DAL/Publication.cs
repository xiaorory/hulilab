using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hulilab.Models.DAL
{
    public class Publication : BaseObject
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Authors { get; set; }
        public string Magazine { get; set; }
        public string PublishYear { get; set; }
        public int CitedBy { get; set; }
    }
}