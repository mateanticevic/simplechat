using SimpleChat.BusinessLayer.Exceptions;
using SimpleChat.BusinessLayer.Extensions.Dto;
using SimpleChat.BusinessLayer.Extensions.Entities;
using SimpleChat.Common.Authentication;
using SimpleChat.Common.Helpers;
using SimpleChat.DataLayer;
using SimpleChat.Model;
using System;

namespace SimpleChat.BusinessLayer
{
    public class BlProfile : BlBase
    {
        public BlProfile(AuthenticationContext authenticationContext) : base(authenticationContext)
        {
        }

        public bool Create(Profile profile)
        {
            try
            {
                var profileEntity = profile.ToEntity();
                profileEntity.PasswordHash = PasswordHelper.GetPasswordHash(profile.Password);

                return DlProfile.Insert(profileEntity);
            }
            catch
            {
                throw new UnknownException();
            }
        }

        public Profile GetMe()
        {
            var profileEntity = DlProfile.GetByNickname(AuthenticationContext.Nickname);

            return profileEntity.ToDto();
        }

        public Profile Get(string nickname)
        {
            try
            {
                return DlProfile.GetByNickname(nickname)
                                .ToDto();
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }
    }
}
