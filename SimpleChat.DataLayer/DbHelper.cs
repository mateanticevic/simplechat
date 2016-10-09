using SimpleChat.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SimpleChat.DataLayer
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["chatSql"].ConnectionString;

            return new SqlConnection(connectionString);
        }

        public static ProfileEntity GetProfile(string nickname)
        {
            using (var db = GetConnection())
            {
                var c = new SqlCommand("ProfileGet", db) { CommandType = System.Data.CommandType.StoredProcedure };
                c.Parameters.Add(new SqlParameter("Nickname", nickname));
                

                db.Open();

                var da = new SqlDataAdapter(c);

                var ds = new DataSet();

                da.Fill(ds);

                return new ProfileEntity()
                {
                    Email = ds.Tables[0].Rows[0][3].ToString(),
                    Nickname = ds.Tables[0].Rows[0][0].ToString(),
                    FirstName = ds.Tables[0].Rows[0][1].ToString(),
                    LastName = ds.Tables[0].Rows[0][2].ToString(),
                    PasswordHash = ds.Tables[0].Rows[0][4].ToString()
                };
            }
        }
    }
}