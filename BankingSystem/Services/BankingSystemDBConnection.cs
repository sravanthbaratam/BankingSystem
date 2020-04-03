using System.Data;
using System.Data.SqlClient;
using BankingSystem.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BankingSystem.Services
{
    public class BankingSystemDBConnection : IBankingSystemDBConnection
    {
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        public BankingSystemDBConnection(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            var connetionString = Configuration["ConnectionStrings"];

            connection = new SqlConnection(connetionString);
            connection.Open();
            return connection;
        }
    }
}
