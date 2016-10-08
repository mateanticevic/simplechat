using System;
using System.Text;

namespace SimpleChat.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string text)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(text);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string FromBase64(this string base64)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}