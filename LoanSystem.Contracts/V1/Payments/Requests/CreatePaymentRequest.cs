namespace LoanSystem.Contracts.V1.Payments.Requests
{
    public record CreatePaymentRequest(decimal Amount, Guid PayerId);
}
