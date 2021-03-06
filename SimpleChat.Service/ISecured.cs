﻿using SimpleChat.Model;
using System.Collections.Generic;
using System.ServiceModel.Web;
using System.ServiceModel;

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
        [WebGet(UriTemplate = "conversation", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Conversation> GetConversations();

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PutConversation();

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}", ResponseFormat = WebMessageFormat.Json)]
        Conversation GetConversation(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}/messages", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Message> GetMessagesByConversation(string identifier);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "conversation/{identifier}/messages", ResponseFormat = WebMessageFormat.Json)]
        object DeleteMessagesByConversation(string identifier);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation/{identifier}/message", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        string PutConversationMessage(string identifier, Message binding);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation/{identifier}/profile", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        object PutConversationProfile(string identifier, Profile profile);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "conversation/{identifier}/seen", RequestFormat = WebMessageFormat.Json)]
        object PutConversationSeen(string identifier);

        [OperationContract]
        [WebInvoke(Method = "DELETE", UriTemplate = "conversation/{identifier}/profile", ResponseFormat = WebMessageFormat.Json)]
        object DeleteConversationProfile(string identifier);

        [OperationContract]
        [WebGet(UriTemplate = "conversation/{identifier}/profiles", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Profile> GetConversationProfiles(string identifier);

        #endregion

        #region Profile

        [OperationContract]
        [WebGet(UriTemplate = "profile", ResponseFormat = WebMessageFormat.Json)]
        Profile GetProfile();

        [OperationContract]
        [WebGet(UriTemplate = "profiles/{searchQuery}", ResponseFormat = WebMessageFormat.Json)]
        IEnumerable<Profile> GetProfiles(string searchQuery);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "profile", RequestFormat = WebMessageFormat.Json)]
        object PostProfile(Profile profile);

        #endregion
    }
}
