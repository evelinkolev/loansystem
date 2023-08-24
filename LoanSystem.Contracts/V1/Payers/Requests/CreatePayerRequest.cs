namespace LoanSystem.Contracts.V1.Payers.Requests
{
    public record CreatePayerRequest(string FullName, decimal Deposit);
}
