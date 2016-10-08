using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleChat.Common.Authentication
{
    public class AuthorizationHeader
    {
        public string Schema { get; set; }
        public string Value { get; set; }
    }
}