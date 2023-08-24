namespace LoanSystem.Contracts.V1.Payers.Responses
{
    public record PayerResponse(
        Guid Id,
        string FullName,
        decimal Deposit,
        string RoutingNumber,
        string AccountNumber,
        DateTime CreatedDateTime,
        Guid UserId);
}
