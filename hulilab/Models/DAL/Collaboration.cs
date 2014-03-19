using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hulilab.Models.DAL
{
    public class Collaboration:BaseObject
    {
        public string EnuFriendName { get; set; }
        public string ChsFriendName { get; set; }
        public string Link { get; set; }
    }
}