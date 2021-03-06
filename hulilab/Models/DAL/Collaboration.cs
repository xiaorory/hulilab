﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace hulilab.Models.DAL
{
    public class Collaboration:BaseObject
    {
        [Required]
        [DisplayName("合作伙伴英文名")]
        public string EnuFriendName { get; set; }
        public string ChsFriendName { get; set; }
        public string Link { get; set; }
    }
}