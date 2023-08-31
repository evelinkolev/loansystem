namespace LoanSystem.Contracts.V1.Cards.Responses
{
    public record CardResponse(
        Guid Id,
        string Number,
        string HolderName,
        string ExpiryDate,
        string SecurityCode,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime,
        Guid PayerId);
}
