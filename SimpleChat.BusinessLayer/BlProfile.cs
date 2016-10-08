using SimpleChat.BusinessLayer.Extensions.Entities;
using SimpleChat.Common.Authentication;
using SimpleChat.DataLayer;
using SimpleChat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.BusinessLayer
{
    public class BlProfile : BlBase
    {
        public BlProfile(AuthenticationContext authenticationContext) : base(authenticationContext)
        {

        }

        public Profile GetMe()
        {
            var profileEntity = DlProfile.GetByNickname(AuthenticationContext.Nickname);

            return profileEntity.ToDto();
        }
    }
}
