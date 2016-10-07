using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using SimpleChat.Model;
using SimpleChat.Service.Authentication;
using System.Runtime.Caching;

namespace SimpleChat.Service
{
    public class Service : IService
    {
        public IEnumerable<Message> GetMessagesByConversation(string identifier)
        {
            return null;
        }

        public IEnumerable<Conversation> GetConversations()
        {
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

        public string IssueToken(IssueTokenBinding binding)
        {
            var cache = MemoryCache.Default;

            var cacheItem = new CacheItem("x123", new AuthenticationContext() { Nickname = binding.Nickname });
            cache.Add(cacheItem, new CacheItemPolicy());

            return "x123";
        }
    }
}
