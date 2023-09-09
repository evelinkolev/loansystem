using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payers.Queries.GetPayerById
{
    public record GetPayerByIdQuery(Guid Id) : IRequest<Payer>;
}
