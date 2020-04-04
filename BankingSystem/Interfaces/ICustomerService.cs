using BankingSystem.Models;
using System;

namespace BankingSystem.Interfaces
{
    public interface ICustomerService
    {
        Response AddCustomer(Account account, Response response);
        Response RemoveCustomer(Account account, Response response);
        Response Authenticate(User user, Response response);
        Response PasswordReset(User user, Response response);
        string GetJsonFileContents(string pathToJson);
        int GenerateRandomNumber(int maxValue);
    }
}
