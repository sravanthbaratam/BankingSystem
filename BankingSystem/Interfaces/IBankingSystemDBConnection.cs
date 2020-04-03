using System.Data.SqlClient;

namespace BankingSystem.Interfaces
{
    public interface IBankingSystemDBConnection
    {
        SqlConnection GetConnections();

        SqlCommand GetCommand(string StoredProcName, SqlConnection connection);

        void CloseConnection(SqlConnection connection);
    }
}
