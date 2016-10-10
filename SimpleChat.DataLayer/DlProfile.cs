using SimpleChat.Common.Extensions;
using SimpleChat.DataLayer.Entities;
using System.Collections.Generic;
using System.Data;
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

        public static bool Insert(ProfileEntity profileEntity)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("Email", profileEntity.Email));
                parameters.Add(new SqlParameter("FirstName", profileEntity.FirstName));
                parameters.Add(new SqlParameter("LastName", profileEntity.LastName));
                parameters.Add(new SqlParameter("Nickname", profileEntity.Nickname));
                parameters.Add(new SqlParameter("PasswordHash", profileEntity.PasswordHash));

                return connection.ExecuteSpBool(StoredProcedureName.ProfileInsert.ToString(), parameters);
            }
        }

        public static IEnumerable<ProfileEntity> Search(string searchQuery)
        {
            using (var connection = DbHelper.GetConnection())
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("SearchQuery", searchQuery));

                var dt = connection.ExecuteSpTable(StoredProcedureName.ProfilesSearch.ToString(), parameters);


                foreach (DataRow row in dt.Rows)
                {
                    yield return new ProfileEntity()
                    {
                        Email = row["Email"].ToString(),
                        Nickname = row["Nickname"].ToString(),
                        FirstName = row["FirstName"].ToString(),
                        LastName = row["LastName"].ToString()
                    };
                }
            }
        }
    }
}
