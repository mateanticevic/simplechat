using System.ServiceModel.Configuration;
using System;

namespace SimpleChat.Service.Handlers
{
    public class AuthenticationHandlerBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get
            {
                return typeof(AuthenticationHandler);
            }
        }

        protected override object CreateBehavior()
        {
            return new AuthenticationHandler();
        }
    }
}