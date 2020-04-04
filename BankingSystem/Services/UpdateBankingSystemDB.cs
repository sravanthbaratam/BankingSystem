using BankingSystem.Interfaces;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace BankingSystem.Services
{
    public class UpdateBankingSystemDB : IUpdateBankingSystemDB
    {
        
        public SqlConnection connection;

        public IBankingSystemDBConnection bankingSystemDBConnection = new BankingSystemDBConnection();

        public void UpdateAccounts(string accountsJson)
        {
            connection = bankingSystemDBConnection.GetConnections();
            string spName = ConfigurationManager.AppSettings["UpdateAccounts"];
            StreamReader sr = new StreamReader(accountsJson);
            string accountsJsonString = sr.ReadToEnd();
            sr.Close();

            var command = bankingSystemDBConnection.GetCommand(spName, connection);
            command.CommandText = command.CommandText;
            command.Parameters.AddWithValue("@accountsJson", accountsJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void UpdateCredentials(string credentialsJson)
        {
            connection = bankingSystemDBConnection.GetConnections();
            string spName = ConfigurationManager.AppSettings["UpdateCredentials"];
            StreamReader sr = new StreamReader(credentialsJson);
            string credentialsJsonString = sr.ReadToEnd();
            sr.Close();

            var command = bankingSystemDBConnection.GetCommand(spName, connection);
            command.Parameters.AddWithValue("@credentialsJson", credentialsJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void UpdateCheque(string chequeJson)
        {
            connection = bankingSystemDBConnection.GetConnections();
            string spName = ConfigurationManager.AppSettings["UpdateCheque"];
            StreamReader sr = new StreamReader(chequeJson);
            string chequeJsonString = sr.ReadToEnd();
            sr.Close();

            var command = bankingSystemDBConnection.GetCommand(spName, connection);
            command.CommandText = command.CommandText;
            command.Parameters.AddWithValue("@chequeJson", chequeJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void UpdateTransactions(string transactionsJson)
        {
            connection = bankingSystemDBConnection.GetConnections();
            string spName = ConfigurationManager.AppSettings["UpdateTransactions"];
            StreamReader sr = new StreamReader(transactionsJson);
            string transactionsJsonString = sr.ReadToEnd();
            sr.Close();

            var command = bankingSystemDBConnection.GetCommand(spName, connection);
            command.CommandText = command.CommandText;
            command.Parameters.AddWithValue("@transactionsJson", transactionsJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
