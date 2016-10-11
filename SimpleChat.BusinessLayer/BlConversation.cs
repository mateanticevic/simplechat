using SimpleChat.BusinessLayer.Exceptions;
using SimpleChat.BusinessLayer.Extensions.Entities;
using SimpleChat.Common.Authentication;
using SimpleChat.DataLayer;
using SimpleChat.Model;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System;

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
                string identifier = DlConversation.Insert(AuthenticationContext.Nickname);
                AddProfileToConversation(AuthenticationContext.Nickname, identifier);

                return identifier;
            }
            catch
            {
                throw new UnknownException();
            }
        }

        public string CreateMessage(string conversationIdentifier, Message message)
        {
            try
            {
                return DlMessage.Insert(AuthenticationContext.Nickname, conversationIdentifier, message.Content);
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public bool DeleteMessage(string messageIdentifier)
        {
            try
            {
                var message = DlMessage.Get(messageIdentifier);

                if (message.Nickname != AuthenticationContext.Nickname)
                {
                    throw new UnauthorizedException();
                }

                return DlMessage.Delete(messageIdentifier);
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public bool DeleteMessages(string conversationIdentifier)
        {
            try
            {
                return DlConversation.DeleteMessages(conversationIdentifier, AuthenticationContext.Nickname);
            }
            catch(Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public bool DeleteProfile(string conversationIdentifier)
        {
            try
            {
                using (var transaction = new TransactionScope())
                {
                    bool success = DlConversation.DeleteMessages(conversationIdentifier, AuthenticationContext.Nickname)
                                   && DlConversation.DeleteProfile(conversationIdentifier, AuthenticationContext.Nickname);

                    transaction.Complete();
                    return success;
                }
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public IEnumerable<Conversation> GetConversations()
        {
            try
            {
                var conversationEntities = DlConversation.GetConversations(AuthenticationContext.Nickname);

                var conversations = new List<Conversation>();

                foreach (var conversationEntity in conversationEntities)
                {
                    var conversation = conversationEntity.ToDto();
                    conversation.Profiles = DlConversation.GetProfiles(conversationEntity.Identifier)
                                                          .Select(x => x.ToDto());
                    conversation.Profiles = conversation.Profiles.Where(x => x.Nickname != AuthenticationContext.Nickname);

                    conversations.Add(conversation);
                }

                return conversations;
            }
            catch
            {
                throw new UnknownException();
            }
        }

        public IEnumerable<Profile> GetConversationProfiles(string conversationIdentifier)
        {
            try
            {
                return DlConversation.GetProfiles(conversationIdentifier)
                                     .Select(x => x.ToDto())
                                     .Where(x => x.Nickname != AuthenticationContext.Nickname);
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public IEnumerable<Message> GetMessages(string conversationIdentifier)
        {
            try
            {
                var messageEntities = DlConversation.GetMessages(conversationIdentifier);

                var messages = new List<Message>();
                foreach (var messageEntity in messageEntities)
                {
                    var message = messageEntity.ToDto();
                    message.Profile = DlProfile.Get(messageEntity.Nickname).ToDto();
                    message.CanDelete = message.Profile.Nickname == AuthenticationContext.Nickname;
                    messages.Add(message);
                }

                return messages;
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public bool Seen(string conversationIdentifier)
        {
            try
            {
                return DlConversationSeen.Insert(AuthenticationContext.Nickname, conversationIdentifier);
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }
    }
}
