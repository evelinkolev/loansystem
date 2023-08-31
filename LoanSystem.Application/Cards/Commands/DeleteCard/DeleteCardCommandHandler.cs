using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Cards.Commands.DeleteCard
{
    internal sealed class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, bool>
    {
        private readonly ICardRepository _cardRepository;

        public DeleteCardCommandHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<bool> Handle(DeleteCardCommand command, CancellationToken cancellationToken)
        {
            var card = await _cardRepository.GetAsync(command.Id) ?? throw new CardNotFoundException(command.Id);

            await _cardRepository.DeleteAsync(card);
            return true;
        }
    }
}
