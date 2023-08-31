namespace LoanSystem.Models.Exceptions
{
    public class InvalidCardExpiryDateException : Exception
    {
        public string? CardExpiryDate { get; }

        public InvalidCardExpiryDateException(string cardExpiryDate) : base($"Invalid card expiry date: '{cardExpiryDate}'.")
        {
            CardExpiryDate = cardExpiryDate;
        }
    }
}
