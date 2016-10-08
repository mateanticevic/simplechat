using SimpleChat.BusinessLayer.Exceptions;
using SimpleChat.BusinessLayer.Extensions.Entities;
using SimpleChat.Common.Authentication;
using SimpleChat.DataLayer;
using SimpleChat.Model.Binding;
using SimpleChat.Model;
using System.Collections.Generic;
using System.Linq;

namespace SimpleChat.BusinessLayer
{
    public class BlConversation : BlBase
    {
        public BlConversation(AuthenticationContext authenticationContext) : base(authenticationContext)
        {
        }

        public bool AddProfileToConversation(string nickname, string conversationIdentifier)
        {
            try
            {
                return DlConversation.InsertConversationProfile(AuthenticationContext.Nickname, nickname, conversationIdentifier);
            }
            catch
            {
                throw new UnknownException();
            }
        }

        public string CreateConversation()
        {
            try
            {
                string identifier = DlConversation.InsertConversation(AuthenticationContext.Nickname);
                AddProfileToConversation(AuthenticationContext.Nickname, identifier);

                return identifier;
            }
            catch
            {
                throw new UnknownException();
            }
        }

        public string CreateMessage(string conversationIdentifier, MessageCreateBinding message)
        {
            string identifier = DlMessage.InsertMessage(AuthenticationContext.Nickname, conversationIdentifier, message.Content);

            return identifier;
        }

        public bool DeleteMessage(string messageIdentifier)
        {
            try
            {
                var message = DlMessage.GetMessage(messageIdentifier);

                if (message.Nickname != AuthenticationContext.Nickname)
                {
                    throw new UnauthorizedException();
                }

                return DlMessage.DeleteMessage(messageIdentifier);
            }
            catch
            {
                throw new EntityNotFoundException();
            }
        }

        public IEnumerable<Message> GetMessages(string conversationIdentifier)
        {
            var messages = DlConversation.GetMessages(conversationIdentifier)
                                         .Select(x => x.ToDto());

            return messages;
        }

        public IEnumerable<Profile> GetConversationProfiles(string conversationIdentifier)
        {
            var profiles = DlConversation.GetProfiles(conversationIdentifier)
                                         .Select(x => x.ToDto());

            return profiles;
        }
    }
}
