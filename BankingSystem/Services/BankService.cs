using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BankingSystem.Services
{
    public class BankService : IBankService
    {
        private IHostingEnvironment _hostingEnv;
        private readonly IUpdateBankingSystemDB _updateBankingSystemDB;
        private readonly ICustomerService _customerService;

        public BankService(IHostingEnvironment hostingEnv, ICustomerService customerService,IUpdateBankingSystemDB updateBankingSystemDB)
        {
            _hostingEnv = hostingEnv ?? throw new ArgumentNullException(nameof(hostingEnv));
            _updateBankingSystemDB = updateBankingSystemDB;
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        public Response AccountSummary(Account account, Response response)
        {
            //Account summary of the specific user
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            try
            {
                string json = _customerService.GetJsonFileContents(accountPath);                    
                if (!string.IsNullOrEmpty(json))
                {
                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                    Account authorized = accounts.Where<Account>(x => x.AccountNumber == account.AccountNumber).FirstOrDefault();

                    if (authorized != null && account.AccountNumber == authorized.AccountNumber)
                    {
                        response.ResponseObject = authorized;
                        response.errorMessage = null;
                        response.Successfull = true;
                    }
                    else
                    {
                        response.Successfull = false;
                        response.ResponseObject = null;
                        response.errorMessage = "Server is busy. We are unable to fetch your records.";
                    }
                }
            }
            catch (Exception exception)
            {
                response.errorMessage = exception.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            return response;
        }

        public Response AddTransaction(Transaction transaction, Response response)
        {
            //update accounts
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);

            fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage//" + transaction.AccountNumber));
            string transactionPath = Path.Combine(fileDirectory, "transactions" + Constants.JsonFormat);

            bool fileExists = false;
            try
            {
                //Adding a new transaction to bank by generating transaction id and deducting it from balance
                fileExists = File.Exists(accountPath);
                if (fileExists)
                {
                    string json = _customerService.GetJsonFileContents(accountPath);
                    if (!string.IsNullOrEmpty(json))
                    {
                        List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                        Account authorized = accounts.Where<Account>(x => x.AccountNumber == transaction.AccountNumber).FirstOrDefault();
                        if (authorized != null)
                        {
                            accounts.Where<Account>(x => x.AccountNumber == transaction.AccountNumber).Select(u => u.Balance = u.Balance - transaction.Amount).ToList();
                            File.WriteAllText(accountPath, JsonConvert.SerializeObject(accounts));

                            //Adding Transaction in the log
                            transaction.TransactionId = _customerService.GenerateRandomNumber(999999);
                            transaction.TimeStamp = DateTime.Now;
                            List<Transaction> transactions = new List<Transaction>();

                            bool folderExists = Directory.Exists(fileDirectory);
                            if (folderExists)
                            {
                                fileExists = File.Exists(transactionPath);
                                if (fileExists)
                                {
                                    transactions = JsonConvert.DeserializeObject<List<Transaction>>(_customerService.GetJsonFileContents(transactionPath));
                                }
                            }
                            else
                            {
                                Directory.CreateDirectory(fileDirectory);
                            }
                            transactions.Add(transaction);
                            File.WriteAllText(transactionPath, JsonConvert.SerializeObject(transactions));
                            response.errorMessage = string.Empty;
                            response.Successfull = true;
                            response.ResponseObject = transaction;
                        }
                        else
                        {
                            response.errorMessage = "We didnt found any customer with the given account number";
                            response.Successfull = false;
                            response.ResponseObject = null;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                response.errorMessage = e.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            _updateBankingSystemDB.UpdateAccounts(accountPath);
            return response;
        }

        public Response ViewTransactions(Account account, Response response)
        {
            //Transactions of the specific user
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            try
            {
                string json = _customerService.GetJsonFileContents(accountPath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                    Account authorized = accounts.Where<Account>(x => x.AccountNumber == account.AccountNumber).FirstOrDefault();

                    if (authorized != null && account.AccountNumber == authorized.AccountNumber)
                    {
                        fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage//" + authorized.AccountNumber));
                        string transactionPath = Path.Combine(fileDirectory, "transactions" + Constants.JsonFormat);
                        json = _customerService.GetJsonFileContents(transactionPath);

                        List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);
                        transactions = transactions.Where<Transaction>(x => x.AccountNumber == account.AccountNumber).ToList();
                        authorized.Transactions = transactions;

                        response.ResponseObject = authorized;
                        response.errorMessage = null;
                        response.Successfull = true;
                    }
                    else
                    {
                        response.Successfull = false;
                        response.ResponseObject = null;
                        response.errorMessage = "Server is busy. We are unable to fetch your records.";
                    }
                }
            }
            catch (Exception exception)
            {
                response.errorMessage = exception.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            return response;
        }

        public Response DateTransactions(Transaction transaction, Response response)
        {
            //Transactions of the specific user
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            try
            {
                string json = _customerService.GetJsonFileContents(accountPath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                    Account authorized = accounts.Where<Account>(x => x.AccountNumber == transaction.AccountNumber).FirstOrDefault();

                    if (authorized != null && transaction.AccountNumber == authorized.AccountNumber)
                    {
                        fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage//" + authorized.AccountNumber));
                        string transactionPath = Path.Combine(fileDirectory, "transactions" + Constants.JsonFormat);
                        json = _customerService.GetJsonFileContents(transactionPath);

                        List<Transaction> transactions = JsonConvert.DeserializeObject<List<Transaction>>(json);
                        transactions = transactions.Where<Transaction>(x => x.AccountNumber == transaction.AccountNumber && x.TimeStamp.Date.Equals(transaction.TimeStamp.Date)).ToList();
                        authorized.Transactions = transactions;

                        response.ResponseObject = authorized;
                        response.errorMessage = null;
                        response.Successfull = true;
                    }
                    else
                    {
                        response.Successfull = false;
                        response.ResponseObject = null;
                        response.errorMessage = "Server is busy. We are unable to fetch your records.";
                    }
                }
            }
            catch (Exception exception)
            {
                response.errorMessage = exception.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            return response;
        }

        public Response ChequeBook(Account account, Response response)
        {
            //update cheque
            //Transactions of the specific user
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            string chequePath = Path.Combine(fileDirectory, "cheque" + Constants.JsonFormat);
            try
            {
                string json = _customerService.GetJsonFileContents(accountPath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                    Account authorized = accounts.Where<Account>(x => x.AccountNumber == account.AccountNumber).FirstOrDefault();
                    if(authorized != null)
                    {
                        List<ChequeBook> chequeBooks = new List<ChequeBook>();
                        ChequeBook chequeBook = new ChequeBook();

                        json = _customerService.GetJsonFileContents(chequePath);                        
                        if (!string.IsNullOrEmpty(json))
                        {
                            chequeBooks = JsonConvert.DeserializeObject<List<ChequeBook>>(json);
                            ChequeBook request = chequeBooks.Where<ChequeBook>(x => x.AccountNumber == authorized.AccountNumber).FirstOrDefault();
                            if(request != null)
                            {
                                string status = string.Empty;
                                if(chequeBook.ChequeBookStatus)
                                {
                                    status = "Approved";
                                }
                                else
                                {
                                    status = "Pending with Bank";
                                }
                                response.errorMessage = "Customer with Request number : " + request.RequestId+" and Account Number : " + account.AccountNumber + " already requested for cheque book and status is : " + status;
                                response.Successfull = false;
                                response.ResponseObject = null;
                            }
                            else
                            {
                                chequeBook.RequestId = _customerService.GenerateRandomNumber(99999);
                                chequeBook.AccountNumber = authorized.AccountNumber;
                                chequeBook.ChequeBookStatus = false;
                                chequeBooks.Add(chequeBook);
                                File.WriteAllText(chequePath, JsonConvert.SerializeObject(chequeBooks));
                                response.errorMessage = string.Empty;
                                response.Successfull = true;
                                response.ResponseObject = null;
                            }
                        }
                        else
                        {
                            chequeBook.RequestId = _customerService.GenerateRandomNumber(99999);
                            chequeBook.AccountNumber = authorized.AccountNumber;
                            chequeBook.ChequeBookStatus = false;
                            chequeBooks.Add(chequeBook);
                            File.WriteAllText(chequePath, JsonConvert.SerializeObject(chequeBooks));
                            response.errorMessage = string.Empty;
                            response.Successfull = true;
                            response.ResponseObject = null;
                        }
                    }
                    else
                    {
                        response.errorMessage = "Customer with Account Number : " + account.AccountNumber + " doesn't exists.";
                        response.Successfull = false;
                        response.ResponseObject = null;
                    }                    
                }
                else
                {
                    response.errorMessage = "Invalid request given by the user.";
                    response.Successfull = false;
                    response.ResponseObject = null;
                }
            }
            catch (Exception exception)
            {
                response.errorMessage = exception.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            _updateBankingSystemDB.UpdateCheque(chequePath);
            _updateBankingSystemDB.UpdateAccounts(accountPath);
            return response;
        }
    }
}
