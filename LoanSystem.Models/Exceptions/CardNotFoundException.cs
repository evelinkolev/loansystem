namespace LoanSystem.Models.Exceptions
{
    public class CardNotFoundException : Exception
    {
        public Guid CardId { get; }

        public CardNotFoundException(Guid cardId):base($"Card with ID: '{cardId}' was not found.")
        {
            CardId = cardId;
        }
    }
}
