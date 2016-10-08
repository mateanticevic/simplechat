using SimpleChat.Common.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Service
{
    [ServiceContract]
    public interface IPublic
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "issueToken", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string IssueToken(IssueTokenBinding binding);
    }
}
