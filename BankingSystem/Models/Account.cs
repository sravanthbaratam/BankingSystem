using System.Collections.Generic;

namespace BankingSystem.Models
{
    public class Account
    {
        public string Name { set; get; }
        public string Pan { set; get; }
        public long AadhaarNumber { set; get; }
        public long AccountNumber { set; get; }
        public int AccountType { set; get; }
        public float Balance { set; get; }
        public string Email { set; get; }
        public string Mobile { set; get; }
        public string Address { set; get; }

        private bool accountStatus;
        public bool AccountStatus
        {
            get { return this.accountStatus; }
            set { this.accountStatus = value; }
        }
        public string ApprovedBy { set; get; }
        public List<Transaction> Transactions { set; get; }
    }

    public class ChequeBook
    {
        public long RequestId { set; get; }        
        public long ChequeBookNumber { set; get; }
        public long AccountNumber { set; get; }
        private bool chequeBookStatus;
        public bool ChequeBookStatus { get => chequeBookStatus; set => chequeBookStatus = value; }
        public string ApprovedBy { set; get; }
    }

    public class User
    {
        public long AccountNumber { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }
    }
}
