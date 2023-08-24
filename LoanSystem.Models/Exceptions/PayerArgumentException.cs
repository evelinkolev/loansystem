namespace LoanSystem.Models.Exceptions
{
    public class PayerArgumentException : Exception
    {
        public string? FullName { get; }
        public decimal Deposit { get; }

        public PayerArgumentException(string fullName) : base($"Payer with Full Name: '{fullName}' is invalid.")
        {
            FullName = fullName;
        }

        public PayerArgumentException(decimal deposit): base($"Payer with Deposit: '{deposit}' is invalid.")
        {
            Deposit = deposit;
        }
    }
}
