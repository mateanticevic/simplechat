using SimpleChat.Common.Authentication;
using SimpleChat.Common.Helpers;
using SimpleChat.DataLayer;
using SimpleChat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System;
using SimpleChat.BusinessLayer;
using System.ServiceModel.Web;
using SimpleChat.Service.Extensions;
using System.Net;

namespace SimpleChat.Service
{
    public class Public : IPublic
    {
        BlProfile blProfile;

        public Public()
        {
            blProfile = new BlProfile(null);
        }

        public object HeadProfile(string nickname)
        {
            var profile = blProfile.Get(nickname);

            if(profile != null)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.OK);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NotFound);
            }

            return null;
        }

        public string IssueToken(IssueTokenBinding binding)
        {
            return blProfile.GetToken(binding.Nickname, binding.Password);
        }

        public object PutProfile(Profile profile)
        {
            bool success = blProfile.Create(profile);

            if(success)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.Created);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.BadRequest);
            }

            return null;
        }
    }
}
