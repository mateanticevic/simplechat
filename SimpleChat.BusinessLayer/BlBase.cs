using SimpleChat.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLayer
{
    public abstract class BlBase
    {
        public BlBase(AuthenticationContext authenticationContext)
        {
            AuthenticationContext = authenticationContext;
        }

        public AuthenticationContext AuthenticationContext { get; private set; }
    }
}
