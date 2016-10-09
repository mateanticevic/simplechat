using SimpleChat.Common.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SimpleChat.DataLayer
{
    public static class DlConversationSeen
    {
        public static bool Insert(string nickname, string conversationIdentifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Nickname", nickname));
                parameters.Add(new SqlParameter("ConversationIdentifier", conversationIdentifier));

                return connection.ExecuteSpBool(StoredProcedureName.ConversationSeenInsert.ToString(), parameters);
            }
        }
    }
}
