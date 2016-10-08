using System.Net;
using System.ServiceModel.Web;

namespace SimpleChat.Service.Extensions
{
    public static class WebOperationContextExtensions
    {
        public static void SetStatusCode(this WebOperationContext context, HttpStatusCode code)
        {
            context.OutgoingResponse.StatusCode = code;
        }
    }
}