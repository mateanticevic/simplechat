﻿using SimpleChat.BusinessLayer;
using SimpleChat.Model.Binding;
using SimpleChat.Model;
using SimpleChat.Service.Extensions;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Web;
using System.ServiceModel;
using System;

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
            return null;
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
    }
}