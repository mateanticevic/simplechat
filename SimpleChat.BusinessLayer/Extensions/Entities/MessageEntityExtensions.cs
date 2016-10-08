using SimpleChat.DataLayer.Entities;
using SimpleChat.Model;

namespace SimpleChat.BusinessLayer.Extensions.Entities
{
    public static class MessageEntityExtensions
    {
        public static Message ToDto(this MessageEntity entity)
        {
            return new Message()
            {
                Content = entity.Content,
                Identifier = entity.Identifier,
                Timestamp = entity.Timestamp
            };
        }
    }
}
