using SimpleChat.DataLayer.Entities;
using SimpleChat.Model;

namespace SimpleChat.BusinessLayer.Extensions.Dto
{
    public static class ProfileExtensions
    {
        public static ProfileEntity ToEntity(this Profile profile)
        {
            return new ProfileEntity()
            {
                Email = profile.Email,
                Nickname = profile.Nickname
            };
        }
    }
}
