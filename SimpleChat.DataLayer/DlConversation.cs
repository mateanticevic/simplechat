using SimpleChat.Common.Extensions;
using SimpleChat.DataLayer.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace SimpleChat.DataLayer
{
    public static class DlConversation
    {
        public static IEnumerable<MessageEntity> GetMessages(string conversationIdentifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", conversationIdentifier));

                var dt = connection.ExecuteSpTable(StoredProcedureName.ConversationMessagesGet.ToString(), parameters);

                foreach (DataRow dataRow in dt.Rows)
                {
                    yield return new MessageEntity()
                    {
                        Content = dataRow["Message"].ToString(),
                        Timestamp = Convert.ToDateTime(dataRow["CreatedOn"]),
                        Identifier = dataRow["Identifier"].ToString(),
                        Nickname = dataRow["Nickname"].ToString()
                    };
                }
            }
        }

        public static IEnumerable<ProfileEntity> GetProfiles(string conversationIdentifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", conversationIdentifier));

                var dt = connection.ExecuteSpTable(StoredProcedureName.ConversationProfilesGet.ToString(), parameters);

                foreach (DataRow dataRow in dt.Rows)
                {
                    yield return new ProfileEntity()
                    {
                        FirstName = dataRow["FirstName"].ToString(),
                        LastName = dataRow["LastName"].ToString(),
                        Nickname = dataRow["Nickname"].ToString()
                    };
                }
            }
        }

        public static string InsertConversation(string nickname)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Nickname", nickname));

                return connection.ExecuteSpString(StoredProcedureName.ConversationInsert.ToString(), parameters);
            }
        }

        public static bool InsertConversationProfile(string nicknameAdder, string nicknameAdded, string conversationIdentifier)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("NicknameAdder", nicknameAdder));
                parameters.Add(new SqlParameter("NicknameAdded", nicknameAdded));
                parameters.Add(new SqlParameter("ConversationIdentifier", conversationIdentifier));

                return connection.ExecuteSpBool(StoredProcedureName.ConversationProfileInsert.ToString(), parameters);
            }
        }
    }
}
