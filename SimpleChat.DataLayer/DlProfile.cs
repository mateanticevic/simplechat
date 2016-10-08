using SimpleChat.DataLayer.Entities;
using System.Collections.Generic;
using SimpleChat.Common.Extensions;
using System.Data.SqlClient;

namespace SimpleChat.DataLayer
{
    public static class DlProfile
    {
        public static ProfileEntity GetByNickname(string nickname)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Nickname", nickname));

                var dr = connection.ExecuteSpRow(StoredProcedureName.ProfileGet.ToString(), parameters);

                return new ProfileEntity()
                {
                    Email = dr["Email"].ToString(),
                    Nickname = dr["Nickname"].ToString(),
                    FirstName = dr["FirstName"].ToString(),
                    LastName = dr["LastName"].ToString(),
                    PasswordHash = dr["PasswordHash"].ToString()
                };
            }
        }
    }
}
