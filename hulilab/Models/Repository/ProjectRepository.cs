﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using hulilab.Models.DAL;

namespace hulilab.Models.Repository
{
    public class MemberRepository : BaseRepository<Member>, IRepository<Member>
    {
    }
}