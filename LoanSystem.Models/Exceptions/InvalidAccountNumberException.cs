namespace LoanSystem.Models.Exceptions
{
    public class InvalidAccountNumberException : Exception
    {
        public string? AccountNumber { get; }

        public InvalidAccountNumberException(string accountNumber) : base($"Invalid account number: '{accountNumber}'.")
        {
            AccountNumber = accountNumber;
        }
    }
}
