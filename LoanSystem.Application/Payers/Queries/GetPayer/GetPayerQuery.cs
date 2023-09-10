using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payers.Queries.GetPayer
{
    public record GetPayerQuery(Guid Id) : IRequest<Payer>;
}
