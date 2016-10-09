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

        public string IssueToken(IssueTokenBinding binding)
        {
            var p = DbHelper.GetProfile(binding.Nickname);

            if (PasswordHelper.IsPasswordHashValid(p.PasswordHash, binding.Password))
            {
                var cache = MemoryCache.Default;

                string token = TokenHelper.NewToken();

                var cacheItem = new CacheItem(token, new AuthenticationContext() { Email = p.Email, Nickname = binding.Nickname });
                cache.Add(cacheItem, new CacheItemPolicy());

                return token;
            }

            return null;
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
