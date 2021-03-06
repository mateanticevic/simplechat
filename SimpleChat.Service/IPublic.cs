﻿using SimpleChat.Common.Authentication;
using SimpleChat.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SimpleChat.Service
{
    [ServiceContract]
    public interface IPublic
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "token", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string PostToken(IssueTokenBinding binding);

        [OperationContract]
        [WebInvoke(Method = "HEAD", UriTemplate = "profile/{nickname}", RequestFormat = WebMessageFormat.Json)]
        object HeadProfile(string nickname);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "profile", RequestFormat = WebMessageFormat.Json)]
        object PutProfile(Profile profile);
    }
}
