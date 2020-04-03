namespace BankingSystem.Interfaces
{
    public interface IUpdateBankingSystemDB
    {    
        void UpdateAccounts(string accountJson);

        void UpdateCredentials(string credentialsJson);

        void UpdateCheque(string chequeJson);

    }
}
