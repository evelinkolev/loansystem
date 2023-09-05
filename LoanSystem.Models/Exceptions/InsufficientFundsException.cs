namespace LoanSystem.Models.Exceptions
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException() : base($"Transaction draws money from a bank account while the account balance is lower than the amount drawn.")
        {
            
        }
    }
}
