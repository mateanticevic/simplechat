using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.ServiceModel;
using System;
using SimpleChat.Service.Extensions;
using System.Runtime.Caching;
using SimpleChat.Common.Authentication;

namespace SimpleChat.Service.Handlers
{
    public class AuthenticationHandler : IDispatchMessageInspector, IServiceBehavior
    {
        void IServiceBehavior.AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        object IDispatchMessageInspector.AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            var authorizationHeader = request.GetAuthorizationHeader();


            var cache = MemoryCache.Default;

            var context = authorizationHeader != null ? (AuthenticationContext)cache.Get(authorizationHeader?.Value) : null;

            if (context != null) request.SetAuthenticationContext(context);

            //request.SetAuthenticationContext(context);

            return request;

            throw new WebFaultException(HttpStatusCode.Unauthorized);
        }

        void IServiceBehavior.ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            

            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher endpointDispatcher in channelDispatcher.Endpoints)
                {
                    //endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);

                    if (endpointDispatcher.EndpointAddress.Uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase) ||
                        endpointDispatcher.EndpointAddress.Uri.Scheme.Equals("https", StringComparison.OrdinalIgnoreCase))
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
                    }
                }
            }
        }

        void IDispatchMessageInspector.BeforeSendReply(ref Message reply, object correlationState)
        {
        }

        void IServiceBehavior.Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}