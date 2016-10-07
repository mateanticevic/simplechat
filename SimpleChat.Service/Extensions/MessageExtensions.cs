using SimpleChat.Service.Authentication;
using System.Linq;
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

        public static string GetMethodName(this Message request)
        {
            if (request.Headers.Action != null)
            {
                return request.Headers.Action.Substring(request.Headers.Action.LastIndexOf('/') + 1);
            }
            else
            {
                return request.Properties.Via.Segments.LastOrDefault();
            }
        }

        public static void SetAuthenticationContext(this Message request, AuthenticationContext context)
        {
            request.Properties.Add(nameof(AuthenticationContext), context);
        }
    }
}
