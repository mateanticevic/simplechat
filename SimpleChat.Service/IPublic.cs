using SimpleChat.Common.Authentication;
using SimpleChat.Model;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace SimpleChat.Service
{
    [ServiceContract]
    public interface IPublic
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "issueToken", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string IssueToken(IssueTokenBinding binding);

        [OperationContract]
        [WebInvoke(Method = "PUT", UriTemplate = "profile", RequestFormat = WebMessageFormat.Json)]
        object PutProfile(Profile profile);
    }
}
