using BankingSystem.Models;

namespace BankingSystem.Interfaces
{
    public interface IBankService
    {
        Response AccountSummary(Account account, Response response);
        Response AddTransaction(Transaction transaction, Response response);
        Response ViewTransactions(Account account, Response response);
        Response DateTransactions(Transaction transaction, Response response);
        Response ChequeBook(Account account, Response response);
    }
}
