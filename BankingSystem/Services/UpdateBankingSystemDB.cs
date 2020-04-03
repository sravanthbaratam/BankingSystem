using BankingSystem.Interfaces;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BankingSystem.Services
{
    public class UpdateBankingSystemDB : IUpdateBankingSystemDB
    {
        private readonly IBankingSystemDBConnection _bankingSystemDBConnection;
        public SqlConnection connection;
        private readonly IConfiguration Configuration;
        
        private UpdateBankingSystemDB(IBankingSystemDBConnection bSDBConnection, SqlConnection connection, IConfiguration configuration)
        {
            _bankingSystemDBConnection = bSDBConnection;
            Configuration = configuration;
            this.connection = connection;        
        }

        public void UpdateAccounts(string accountsJson)
        {
            string spName = Convert.ToString(Configuration["UpdateAccounts"]);
            StreamReader sr = new StreamReader(accountsJson);
            string accountsJsonString = sr.ReadToEnd();

            var command = _bankingSystemDBConnection.GetCommand(spName, connection);
            command.Parameters.AddWithValue("@accountsJson", accountsJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void UpdateCredentials(string credentialsJson)
        {
            string spName = Convert.ToString(Configuration["UpdateCredentials"]);
            StreamReader sr = new StreamReader(credentialsJson);
            string credentialsJsonString = sr.ReadToEnd();

            var command = _bankingSystemDBConnection.GetCommand(spName, connection);
            command.Parameters.AddWithValue("@credentialsJson", credentialsJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }

        public void UpdateCheque(string chequeJson)
        {
            string spName = Convert.ToString(Configuration["UpdateCheque"]);
            StreamReader sr = new StreamReader(chequeJson);
            string chequeJsonString = sr.ReadToEnd();

            var command = _bankingSystemDBConnection.GetCommand(spName, connection);
            command.Parameters.AddWithValue("@chequeJson", chequeJsonString);
            command.ExecuteNonQuery();
            command.Dispose();
        }
    }
}
