using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Cards.Queries.GetCard
{
    public record GetCardQuery(Guid Id) : IRequest<Card>;
}
