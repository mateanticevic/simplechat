using SimpleChat.Common.Authentication;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel;

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
    }
}