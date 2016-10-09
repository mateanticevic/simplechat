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
        public static bool DeleteMessages(string conversationIdentifier, string nickname)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", conversationIdentifier));
                parameters.Add(new SqlParameter("Nickname", nickname));

                return connection.ExecuteSpBool(StoredProcedureName.ConversationMessagesDelete.ToString(), parameters);
            }
        }

        public static bool DeleteProfile(string conversationIdentifier, string nickname)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Identifier", conversationIdentifier));
                parameters.Add(new SqlParameter("Nickname", nickname));

                return connection.ExecuteSpBool(StoredProcedureName.ConversationProfileDelete.ToString(), parameters);
            }
        }

        public static IEnumerable<ConversationEntity> GetConversations(string nickname)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Nickname", nickname));

                var dt = connection.ExecuteSpTable(StoredProcedureName.ConversationsGet.ToString(), parameters);

                foreach (DataRow dr in dt.Rows)
                {
                    yield return new ConversationEntity()
                    {
                        Identifier = dr["Identifier"].ToString(),
                        LastActivity = dr["LastMessage"] is DBNull ? null : (DateTime?)Convert.ToDateTime(dr["LastMessage"]),
                        NewMessages = Convert.ToInt32(dr["NewMessages"])
                    };
                }
            }
        }

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
