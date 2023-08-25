namespace LoanSystem.Models.Exceptions
{
    public class PayerNotFoundException : Exception
    {
        public Guid PayerId { get; }

        public PayerNotFoundException(Guid payerId) : base($"Payer with ID: '{payerId}' was not found.")
        {
            PayerId = payerId;
        }
    }
}
