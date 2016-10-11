using SimpleChat.Common.Authentication;
using System.Net;
using System.ServiceModel.Channels;
using System.Text.RegularExpressions;

namespace SimpleChat.Service.Extensions
{
    public static class MessageExtensions
    {
        public static string GetAuthenticationHeader(this Message request)
        {
            var message = (HttpRequestMessageProperty)request.Properties[HttpRequestMessageProperty.Name];

            return message.Headers[HttpRequestHeader.Authorization];
        }

        public static AuthorizationHeader GetAuthorizationHeader(this Message request)
        {
            string header = request.GetAuthenticationHeader();

            if (!string.IsNullOrEmpty(header) && new Regex("^[^ ]+ [^ ]+$").IsMatch(header))
            {
                string schema = header.Substring(0, header.IndexOf(' ')); ;
                string value = header.Substring(header.IndexOf(' ') + 1);

                return new AuthorizationHeader() { Schema = schema, Value = value };
            }
            else
            {
                return null;
            }
        }

        public static void SetAuthenticationContext(this Message request, AuthenticationContext context)
        {
            request.Properties.Add(nameof(AuthenticationContext), context);
        }
    }
}
