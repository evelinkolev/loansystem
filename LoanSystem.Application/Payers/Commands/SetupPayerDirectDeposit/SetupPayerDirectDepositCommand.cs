using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payers.Commands.SetupPayerDirectDeposit
{
    public record SetupPayerDirectDepositCommand(
        Guid PayerId,
        decimal Deposit,
        string RoutingNumber,
        string AccountNumber) : IRequest<Payer>;
}
