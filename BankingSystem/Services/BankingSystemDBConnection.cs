using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using BankingSystem.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BankingSystem.Services
{
    public class BankingSystemDBConnection : IBankingSystemDBConnection
    {

        public void CloseConnection(SqlConnection connection)
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.Dispose();
            }
        }

        public SqlCommand GetCommand(string StoredProcName, SqlConnection connection)
        {
            SqlCommand cmd = new SqlCommand(StoredProcName, connection);
            cmd.CommandType = CommandType.StoredProcedure;

            return cmd;
        }

        public SqlConnection GetConnections()
        {
            SqlConnection connection;
            var connetionString = ConfigurationManager.AppSettings["bSDBConnection"].ToString();

            connection = new SqlConnection(connetionString);
            connection.Open();
            return connection;
        }
    }
}
