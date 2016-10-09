using SimpleChat.BusinessLayer;
using SimpleChat.Model.Binding;
using SimpleChat.Model;
using SimpleChat.Service.Extensions;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using System.ServiceModel;
using System;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.IO;
using Newtonsoft.Json;

namespace SimpleChat.Service
{
    public class Secured : ISecured
    {
        BlConversation blConversation;
        BlProfile blProfile;

        public Secured()
        {
            var context = OperationContext.Current.GetAuthenticationContext();
            blConversation = new BlConversation(context);
            blProfile = new BlProfile(context);
        }

        public IEnumerable<Message> GetMessagesByConversation(string identifier)
        {
            return blConversation.GetMessages(identifier);
        }

        public IEnumerable<Conversation> GetConversations()
        {
            var conversations = blConversation.GetConversations();

            string ifNoneMatch = WebOperationContext.Current.IncomingRequest.Headers.Get("If-None-Match");

            string etag = GetHash(conversations);

            if (ifNoneMatch != etag)
            {
                WebOperationContext.Current.OutgoingResponse.ETag = etag;
                return conversations;
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NotModified);
                return null;
            }
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

        public IEnumerable<Profile> GetConversationProfiles(string identifier)
        {
            return blConversation.GetConversationProfiles(identifier);
        }

        public string PutConversationMessage(string identifier, MessageCreateBinding binding)
        {
            string messageIdentifier = blConversation.CreateMessage(identifier, binding);

            return messageIdentifier;
        }

        public object DeleteMessage(string identifier)
        {
            bool success = blConversation.DeleteMessage(identifier);

            if(success)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NotFound);
            }

            return null;
        }

        public string PutConversation()
        {
            return blConversation.CreateConversation();
        }

        public object PutConversationProfile(string identifier, Profile profile)
        {
            bool success = blConversation.AddProfileToConversation(profile.Nickname, identifier);

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

        public object DeleteMessagesByConversation(string identifier)
        {
            bool success = blConversation.DeleteMessages(identifier);

            if(success)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return null;
        }

        public object PutConversationSeen(string identifier)
        {
            bool success = blConversation.Seen(identifier);

            if(success)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.Created);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return null;
        }

        public object DeleteConversationProfile(string identifier)
        {
            bool success = blConversation.DeleteProfile(identifier);

            if (success)
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                WebOperationContext.Current.SetStatusCode(HttpStatusCode.InternalServerError);
            }

            return null;
        }

        public object PostProfile(Profile profile)
        {
            throw new NotImplementedException();
        }

        public static string GetHash(object value)
        {
            string json = JsonConvert.SerializeObject(value);

            var Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(json));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString().Substring(0,6);
        }
    }
}
