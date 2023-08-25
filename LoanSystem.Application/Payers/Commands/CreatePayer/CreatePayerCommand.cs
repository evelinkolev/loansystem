using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payers.Commands.CreatePayer
{
    public record CreatePayerCommand(string FullName, decimal Deposit) : IRequest<Payer>;
}
