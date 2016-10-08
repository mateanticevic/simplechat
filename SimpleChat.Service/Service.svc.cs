using System;
using System.Collections.Generic;
using System.ServiceModel;
using SimpleChat.Model;
using SimpleChat.Service.Extensions;
using System.Runtime.Caching;
using SimpleChat.DataLayer;
using SimpleChat.Common.Helpers;
using SimpleChat.Common.Authentication;
using SimpleChat.BusinessLayer;

namespace SimpleChat.Service
{
    public class Service : IService
    {
        BlProfile blProfile;

        public Service()
        {
            var context = OperationContext.Current.GetAuthenticationContext();
            blProfile = new BlProfile(context);
        }

        public IEnumerable<Message> GetMessagesByConversation(string identifier)
        {
            return null;
        }

        public IEnumerable<Conversation> GetConversations()
        {
            var context = OperationContext.Current.GetAuthenticationContext();

            var p = DbHelper.GetConversation(context.Nickname);

            var conv = new Conversation()
            {
                Identifier = "sfsf"
            };

            var l = new List<Conversation>();

            l.Add(conv);

            return l;
        }

        public Conversation GetConversation(string identifier)
        {
            var conv = new Conversation()
            {
                Identifier = "sfsf",
                LastActivity = DateTime.Now
            };

            return conv;
        }

        public Profile GetProfile()
        {
            return blProfile.GetMe();
        }

        public string IssueToken(IssueTokenBinding binding)
        {
            var p = DbHelper.GetProfile(binding.Nickname);

            if(PasswordHelper.IsPasswordHashValid(p.PasswordHash, binding.Password))
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
