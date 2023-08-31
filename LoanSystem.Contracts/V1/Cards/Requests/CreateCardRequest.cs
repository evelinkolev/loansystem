namespace LoanSystem.Contracts.V1.Cards.Requests
{
    public record CreateCardRequest(string HolderName, string ExpiryDate, Guid PayerId);
}
