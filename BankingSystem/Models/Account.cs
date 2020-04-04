using System.Collections.Generic;

namespace BankingSystem.Models
{
    public class Account
    {
        public string Name { set; get; }
        public string Pan { set; get; }
        public long AadhaarNumber { set; get; }
        public int AccountNumber { set; get; }
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Account objAsPart = obj as Account;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return AccountNumber;
        }
        public bool Equals(Account other)
        {
            if (other == null) return false;
            return (this.AccountNumber.Equals(other.AccountNumber));
        }
    }

    public class ChequeBook
    {
        public long RequestId { set; get; }        
        public long ChequeBookNumber { set; get; }
        public int AccountNumber { set; get; }
        private bool chequeBookStatus;
        public bool ChequeBookStatus { get => chequeBookStatus; set => chequeBookStatus = value; }
        public string ApprovedBy { set; get; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Account objAsPart = obj as Account;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return AccountNumber;
        }
        public bool Equals(Account other)
        {
            if (other == null) return false;
            return (this.AccountNumber.Equals(other.AccountNumber));
        }
    }

    public class User
    {
        public int AccountNumber { set; get; }
        public string Password { set; get; }
        public string Role { set; get; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            Account objAsPart = obj as Account;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return AccountNumber;
        }
        public bool Equals(Account other)
        {
            if (other == null) return false;
            return (this.AccountNumber.Equals(other.AccountNumber));
        }
    }
}
