using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using SimpleChat.Common.Authentication;
using SimpleChat.DataLayer;
using SimpleChat.Common.Helpers;
using System.Runtime.Caching;

namespace SimpleChat.Service
{
    public class Public : IPublic
    {
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
    }
}
