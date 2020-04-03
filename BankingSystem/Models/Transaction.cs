using System;

namespace BankingSystem.Models
{
    public class Transaction
    {
        public long TransactionId { set; get; }
        public long AccountNumber { set; get; }
        public int TransactionType { set; get; }
        public float Amount { set; get; }
        public long RecieverAccount { set; get; }
        public DateTime TimeStamp { set; get; }

    }

    public static class Constants
    {
        public const string FileDateTimeFormat = "_yyyyMMdd_HHmmss";
        public const string JsonFormat = ".json";
        public const int MAXIMUM_IDENTICAL_CONSECUTIVE_CHARS = 2;
        public const string LOWERCASE_CHARACTERS = "abcdefghijklmnopqrstuvwxyz";
        public const string UPPERCASE_CHARACTERS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string NUMERIC_CHARACTERS = "0123456789";
        public const string SPECIAL_CHARACTERS = @"!#$%&*@\";
        public const string SPACE_CHARACTER = " ";
        public const int PASSWORD_LENGTH_MIN = 8;
        public const int PASSWORD_LENGTH_MAX = 24;
        public const string REGEX_LOWERCASE = @"[a-z]";
        public const string REGEX_UPPERCASE = @"[A-Z]";
        public const string REGEX_NUMERIC = @"[\d]";
        public const string REGEX_SPECIAL = @"([!#$%&*@\\])+";
        public const string REGEX_SPACE = @"([ ])+";
    }

    public class Response
    {
        private bool successfull;
        public Object ResponseObject { get; set; }
        public string errorMessage { get; set; }
        public bool Successfull { get => successfull; set => successfull = value; }
    }
}
// CurrentDate = Convert.ToDateTime(DateTime.Now.ToString("dd-MMM-yyyy"));