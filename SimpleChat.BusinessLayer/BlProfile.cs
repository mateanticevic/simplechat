using SimpleChat.BusinessLayer.Exceptions;
using SimpleChat.BusinessLayer.Extensions.Dto;
using SimpleChat.BusinessLayer.Extensions.Entities;
using SimpleChat.Common.Authentication;
using SimpleChat.Common.Helpers;
using SimpleChat.DataLayer;
using SimpleChat.Model;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Caching;

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

        public Profile Get(string nickname)
        {
            try
            {
                return DlProfile.Get(nickname)
                                .ToDto();
            }
            catch (Exception e)
            {
                throw new EntityNotFoundException(e);
            }
        }

        public Profile GetCurrent()
        {
            return Get(AuthenticationContext.Nickname);
        }

        public string GetToken(string nickname, string password)
        {
            try
            {
                var profile = DlProfile.Get(nickname);

                if (PasswordHelper.IsPasswordHashValid(profile.PasswordHash, password))
                {
                    var cache = MemoryCache.Default;

                    string token = TokenHelper.NewToken();

                    var cacheItem = new CacheItem(token, new AuthenticationContext() { Email = profile.Email, Nickname = nickname });
                    cache.Add(cacheItem, new CacheItemPolicy());

                    return token;
                }

                throw new UnauthorizedException();
            }
            catch (Exception e)
            {
                throw new UnauthorizedException(e);
            }
        }

        public IEnumerable<Profile> Search(string searchQuery)
        {
            try
            {
                return DlProfile.Search(searchQuery)
                                .Select(x => x.ToDto());
            }
            catch
            {
                throw new UnknownException();
            }
        }
    }
}
