using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Cards.Commands.CreateCard
{
    public record CreateCardCommand(string HolderName, string ExpiryDate, Guid PayerId) : IRequest<Card>;
}
