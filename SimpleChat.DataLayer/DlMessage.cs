using SimpleChat.Common.Extensions;
using SimpleChat.DataLayer.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace SimpleChat.DataLayer
{
    public class DlMessage
    {
        public static bool Delete(string identifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", identifier));

                return connection.ExecuteSpBool(StoredProcedureName.MessageDelete.ToString(), parameters);
            }
        }

        public static MessageEntity Get(string identifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", identifier));

                var dr = connection.ExecuteSpRow(StoredProcedureName.MessageGet.ToString(), parameters);

                return new MessageEntity()
                {
                    Content = dr["Content"].ToString(),
                    Timestamp = Convert.ToDateTime(dr["Timestamp"]),
                    Identifier = dr["Identifier"].ToString(),
                    Nickname = dr["Nickname"].ToString()
                };
            }
        }

        public static string Insert(string nickname, string conversationIdentifier, string message)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Nickname", nickname));
                parameters.Add(new SqlParameter("ConversationIdentifier", conversationIdentifier));
                parameters.Add(new SqlParameter("Message", message));

                var dr = connection.ExecuteSpRow(StoredProcedureName.MessageInsert.ToString(), parameters);

                return dr["Identifier"].ToString();
            }
        }
    }
}
