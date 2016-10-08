using SimpleChat.Common.Extensions;
using System;

namespace SimpleChat.Common.Helpers
{
    public class TokenHelper
    {
        public static string NewToken()
        {
            return Guid.NewGuid()
                       .ToString()
                       .Replace("-", string.Empty)
                       .ToBase64();
        }
    }
}