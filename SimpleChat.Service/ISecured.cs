using SimpleChat.Common.Authentication;
using SimpleChat.Model;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.ServiceModel;
using SimpleChat.Model.Binding;

namespace SimpleChat.Service
{
    [ServiceContract]
    public interface ISecured
    {
        #region Conversation

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "message/{identifier}", ResponseFormat = WebMessageFormat.Json)]
        object DeleteMessage(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}", ResponseFormat = WebMessageFormat.Json)]
        Conversation GetConversation(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}/profiles", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Profile> GetConversationProfiles(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "conversation", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Conversation> GetConversations();

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}/messages", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Message> GetMessagesByConversation(string identifier);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PutConversation();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation/{identifier}/message", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PutConversationMessage(string identifier, MessageCreateBinding binding);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation/{identifier}/profile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        object PutConversationProfile(string identifier, Profile profile);

        #endregion

        #region Profile

        [OperationContract]
        [WebGet(UriTemplate = "profile", ResponseFormat = WebMessageFormat.Json)]
        Profile GetProfile();

        #endregion
    }
}
