﻿using SimpleChat.DataLayer.Entities;
using SimpleChat.Model;

namespace SimpleChat.BusinessLayer.Extensions.Entities
{
    public static class ProfileEntityExtensions
    {
        public static Profile ToDto(this ProfileEntity entity)
        {
            return new Profile()
            {
                Email = entity.Email,
                Nickname = entity.Nickname
            };
        }
    }
}
