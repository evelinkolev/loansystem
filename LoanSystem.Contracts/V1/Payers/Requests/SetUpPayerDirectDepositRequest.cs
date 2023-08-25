namespace LoanSystem.Contracts.V1.Payers.Requests
{
    public record SetUpPayerDirectDepositRequest(decimal Deposit, string RoutingNumber, string AccountNumber);

}
