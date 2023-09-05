namespace LoanSystem.Contracts.V1.Payments.Responses
{
    public record PaymentResponse(
        Guid Id,
        decimal Amount,
        string RequestType,
        Guid PayerId,
        DateTime CreatedDateTime,
        DateTime UpdatedDateTime);
}
