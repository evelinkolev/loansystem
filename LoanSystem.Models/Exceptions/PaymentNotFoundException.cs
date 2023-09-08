namespace LoanSystem.Models.Exceptions
{
    public class PaymentNotFoundException : Exception
    {
        public Guid Id { get; }

        public PaymentNotFoundException(Guid id) : base($"Payment with ID: '{id}' was not found.")
        {
            Id = id;
        }
    }
}
