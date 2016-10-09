using SimpleChat.DataLayer.Entities;
using SimpleChat.Model;

namespace SimpleChat.BusinessLayer.Extensions.Entities
{
    public static class ConversationEntityExtensions
    {
        public static Conversation ToDto(this ConversationEntity entity)
        {
            return new Conversation()
            {
                NewMessages = entity.NewMessages,
                Identifier = entity.Identifier,
                LastActivity = entity.LastActivity
            };
        }
    }
}
