using SimpleChat.BusinessLayer.Exceptions;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System;

namespace SimpleChat.Service.Handlers
{
    public class ExceptionHandler : IErrorHandler
    {
        public bool HandleError(Exception e)
        {
            return true;
        }

        public void ProvideFault(Exception e, MessageVersion version, ref Message fault)
        {
            if(e is WebFaultException)
            {
                var we = (WebFaultException)e;
                SetStatus(we.StatusCode);
            }
            else if(e is UnauthorizedException)
            {
                SetStatus(HttpStatusCode.Unauthorized);
            }
            else if(e is EntityNotFoundException)
            {
                SetStatus(HttpStatusCode.NotFound);
            }
            else if(e is UnknownException)
            {
                SetStatus(HttpStatusCode.InternalServerError);
            }
        }

        private void SetStatus(HttpStatusCode status)
        {
            WebOperationContext.Current.OutgoingResponse.StatusCode = status;
            WebOperationContext.Current.OutgoingResponse.SuppressEntityBody = true;
        }
    }
}