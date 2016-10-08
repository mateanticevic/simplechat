using SimpleChat.Common.Authentication;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SimpleChat.Service.Extensions
{
    public static class OperationContextExtensions
    {
        public static AuthenticationContext GetAuthenticationContext(this OperationContext context)
        {
            var authenticationContextObject = context.RequestContext.RequestMessage.Properties.SingleOrDefault(x => x.Key == nameof(AuthenticationContext));

            var authenticationContext = (AuthenticationContext)authenticationContextObject.Value;

            return authenticationContext;
        }

        public static string GetIpAddress(this OperationContext context)
        {
            MessageProperties messageProperties = context.IncomingMessageProperties;
            var endpointProperty = messageProperties[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;

            return endpointProperty.Address;
        }
    }
}