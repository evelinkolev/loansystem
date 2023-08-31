namespace LoanSystem.Models.Exceptions
{
    public class InvalidCardHolderNameException : Exception
    {
        public string? CardHolderName { get;}

        public InvalidCardHolderNameException(string cardHolderName) : base($"Invalid card holder name: '{cardHolderName}'.")
        {
            CardHolderName = cardHolderName;
        }
    }
}
