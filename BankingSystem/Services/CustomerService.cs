using BankingSystem.Interfaces;
using BankingSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
namespace BankingSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private IHostingEnvironment _hostingEnv;
        private IUpdateBankingSystemDB _updateBankingSystemDB;
        public CustomerService(IHostingEnvironment hostingEnv,IUpdateBankingSystemDB updateBankingSystemDB)
        {
            _hostingEnv = hostingEnv ?? throw new ArgumentNullException(nameof(hostingEnv));
            _updateBankingSystemDB = updateBankingSystemDB;
        }

        public Response AddCustomer(Account account, Response response)
        {
            //update accounts and credentials
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            string credentialPath = Path.Combine(fileDirectory, "credentials" + Constants.JsonFormat);
            try
            {
                //Adding a new customer to bank by generating account number, balance and password
                account.AccountNumber = GenerateRandomNumber(999999999);
                account.Balance = GenerateRandomNumber(999999);

                //User Credentials
                User user = new User();
                user.AccountNumber = account.AccountNumber;
                user.Password = GeneratePassword();
                user.Role = "user";

                //response.AccountNumber = user.AccountNumber;
                //response.Password = user.Password;

                bool folderExists = Directory.Exists(fileDirectory);
                if (folderExists)
                {
                    bool fileExists = File.Exists(accountPath);
                    List<Account> accounts = new List<Account>();
                    if (fileExists)
                    {                        
                        accounts = JsonConvert.DeserializeObject<List<Account>>(GetJsonFileContents(accountPath));
                    }
                    accounts.Add(account);
                    File.WriteAllText(accountPath, JsonConvert.SerializeObject(accounts));

                    fileExists = File.Exists(credentialPath);
                    List<User> users = new List<User>();
                    if (fileExists)
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(GetJsonFileContents(credentialPath));
                    }
                    users.Add(user);
                    File.WriteAllText(credentialPath, JsonConvert.SerializeObject(users));
                }

                response.Successfull = true;
                response.ResponseObject = user;
            }
            catch (Exception e)
            {
                response.errorMessage = e.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            _updateBankingSystemDB.UpdateAccounts(accountPath);
            _updateBankingSystemDB.UpdateCredentials(credentialPath);            
            return response;
        }

        public Response RemoveCustomer(Account account, Response response)
        {
            //update all
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string accountPath = Path.Combine(fileDirectory, "accounts" + Constants.JsonFormat);
            string credentialPath = Path.Combine(fileDirectory, "credentials" + Constants.JsonFormat);
            string chequePath = Path.Combine(fileDirectory, "cheque" + Constants.JsonFormat);
            try
            {
                User user = new User();
                user.AccountNumber = account.AccountNumber;
                ChequeBook cheque = new ChequeBook();
                cheque.AccountNumber = account.AccountNumber;

                bool folderExists = Directory.Exists(fileDirectory);
                if (folderExists)
                {
                    bool fileExists = File.Exists(accountPath);
                    List<Account> accounts = new List<Account>();
                    if (fileExists)
                    {
                        accounts = JsonConvert.DeserializeObject<List<Account>>(GetJsonFileContents(accountPath));
                    }
                    accounts.Remove(account);
                    File.WriteAllText(accountPath, JsonConvert.SerializeObject(accounts));

                    fileExists = File.Exists(credentialPath);
                    List<User> users = new List<User>();
                    if (fileExists)
                    {
                        users = JsonConvert.DeserializeObject<List<User>>(GetJsonFileContents(credentialPath));
                    }
                    users.Remove(user);
                    File.WriteAllText(credentialPath, JsonConvert.SerializeObject(users));

                    fileExists = File.Exists(chequePath);
                    List<ChequeBook> chequeBooks = new List<ChequeBook>();
                    if (fileExists)
                    {
                        chequeBooks = JsonConvert.DeserializeObject<List<ChequeBook>>(GetJsonFileContents(chequePath));
                    }
                    chequeBooks.Remove(cheque);
                    File.WriteAllText(chequePath, JsonConvert.SerializeObject(chequeBooks));
                }

                response.Successfull = true;
                response.ResponseObject = user;
            }
            catch (Exception e)
            {
                response.errorMessage = e.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }
            _updateBankingSystemDB.UpdateAccounts(accountPath);
            _updateBankingSystemDB.UpdateCheque(chequePath);
            _updateBankingSystemDB.UpdateCredentials(credentialPath);
            return response;
        }

        public Response Authenticate(User user, Response response)
        {
            //Authenticating users
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string credentialPath = Path.Combine(fileDirectory, "credentials" + Constants.JsonFormat);
            try
            {               
                string json = GetJsonFileContents(credentialPath);
                if(!string.IsNullOrEmpty(json))
                {
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(json);                    
                    User authorized = users.Where<User>(x => (x.AccountNumber == user.AccountNumber) && (x.Password.Equals(x.Password))).FirstOrDefault();
                    if(authorized != null && (user.AccountNumber == authorized.AccountNumber) && (user.Password.Equals(authorized.Password)))
                    {
                        response.ResponseObject = authorized.Role;
                        response.errorMessage = null;
                        response.Successfull = true;
                    }
                    else
                    {
                        response.Successfull = false;
                        response.ResponseObject = null;
                        response.errorMessage = "Invalid account number or password";
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

        public Response PasswordReset(User user, Response response)
        {
            //Self password reset of users
            //update credentials
            string fileDirectory = Path.Combine(_hostingEnv.ContentRootPath, string.Format("Storage"));
            string credentialPath = Path.Combine(fileDirectory, "credentials" + Constants.JsonFormat);
            try
            {
                string json = GetJsonFileContents(credentialPath);
                if (!string.IsNullOrEmpty(json))
                {
                    List<User> users = JsonConvert.DeserializeObject<List<User>>(json);
                    User authorized = users.Where<User>(x => x.AccountNumber == user.AccountNumber).FirstOrDefault();
                    if (authorized != null)
                    {
                        users.Where<User>(x => x.AccountNumber == user.AccountNumber).Select(u => u.Password = user.Password).ToList();
                        File.WriteAllText(credentialPath, JsonConvert.SerializeObject(users));
                        response.errorMessage = string.Empty;
                        response.Successfull = true;
                        response.ResponseObject = null;
                    }
                    else
                    {
                        response.errorMessage = "We didnt found any customer with the given account number";
                        response.Successfull = false;
                        response.ResponseObject = null;
                    }
                }
            }
            catch (Exception exception)
            {
                response.errorMessage = exception.Message;
                response.Successfull = false;
                response.ResponseObject = null;
            }

            _updateBankingSystemDB.UpdateCredentials(credentialPath);
            return response;
        }

        private string GeneratePassword()
        {
            //To generate a random password for customer
            string characterSet = Constants.LOWERCASE_CHARACTERS + Constants.UPPERCASE_CHARACTERS + Constants.SPECIAL_CHARACTERS + Constants.NUMERIC_CHARACTERS;
            int characterSetLength = characterSet.Length;

            char[] password = new char[Constants.PASSWORD_LENGTH_MIN];
            Random random = new Random();
            for (int characterPosition = 0; characterPosition < Constants.PASSWORD_LENGTH_MIN; characterPosition++)
            {
                password[characterPosition] = characterSet[random.Next(characterSetLength - 1)];

                bool moreThanTwoIdenticalInARow =
                    characterPosition > Constants.MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS
                    && password[characterPosition] == password[characterPosition - 1]
                    && password[characterPosition - 1] == password[characterPosition - 2];

                if (moreThanTwoIdenticalInARow)
                {
                    characterPosition--;
                }
            }
            return string.Join(null, password); ;
        }

        private bool ValidatePassword(string password)
        {
            //To validate a password
            return Regex.IsMatch(password, Constants.REGEX_LOWERCASE)
                && Regex.IsMatch(password, Constants.REGEX_UPPERCASE)
                && Regex.IsMatch(password, Constants.REGEX_NUMERIC)
                && Regex.IsMatch(password, Constants.REGEX_SPECIAL)
                && !Regex.IsMatch(password, Constants.REGEX_SPACE)
                && password.Length >= Constants.PASSWORD_LENGTH_MIN
                && password.Length <= Constants.PASSWORD_LENGTH_MAX;
        }

        public int GenerateRandomNumber(int maxValue)
        {
            Random random = new Random();
            return random.Next(maxValue);
        }

        public string GetJsonFileContents(string pathToJson)
        {
            //To read data from a JSON File            
            string json = string.Empty;
            try
            {
                bool fileExists = File.Exists(pathToJson);
                if (fileExists)
                {
                    using (StreamReader reader = System.IO.File.OpenText(pathToJson))
                    {
                        json = reader.ReadToEnd();
                        json = JToken.Parse(json).ToString();
                    }                    
                }
                else
                {
                    json = string.Empty;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                json = string.Empty;
            }
            return json;
        }
    }
}