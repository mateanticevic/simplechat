using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleChat.Common.Extensions
{
    public static class SqlConnectionExtensions
    {
        private static DataSet ExecuteSp(this SqlConnection connection, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }

            var c = new SqlCommand(procedureName, connection) { CommandType = CommandType.StoredProcedure };
            c.Parameters.AddRange(parameters.ToArray());

            var da = new SqlDataAdapter(c);

            var ds = new DataSet();

            da.Fill(ds);

            return ds;
        }

        public static bool ExecuteSpBool(this SqlConnection connection, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            return Convert.ToBoolean(ExecuteSpRow(connection, procedureName, parameters)[0]);
        }

        public static DataTable ExecuteSpTable(this SqlConnection connection, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            return ExecuteSp(connection, procedureName, parameters).Tables[0];
        }

        public static DataRow ExecuteSpRow(this SqlConnection connection, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            return ExecuteSp(connection, procedureName, parameters).Tables[0].Rows[0];
        }

        public static string ExecuteSpString(this SqlConnection connection, string procedureName, IEnumerable<SqlParameter> parameters)
        {
            return ExecuteSp(connection, procedureName, parameters).Tables[0].Rows[0][0].ToString();
        }
    }
}
