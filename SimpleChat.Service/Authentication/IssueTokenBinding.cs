﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Service.Authentication
{
    public class IssueTokenBinding
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
    }
}