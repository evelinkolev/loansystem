using MediatR;

namespace LoanSystem.Application.Cards.Commands.DeleteCard
{
    public record DeleteCardCommand(Guid Id) : IRequest<bool>;
}
