using SimpleChat.Common.Authentication;
using SimpleChat.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SimpleChat.Service
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}", ResponseFormat = WebMessageFormat.Json)]
        Conversation GetConversation(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "profile", ResponseFormat = WebMessageFormat.Json)]
        Profile GetProfile();

        [OperationContract]
        [WebGet(UriTemplate = "conversation", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Conversation> GetConversations();

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}/messages", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Message> GetMessagesByConversation(string identifier);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "issueToken", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string IssueToken(IssueTokenBinding binding);
    }
}
