using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Cards.Queries.GetCard
{
    internal sealed class GetCardQueryHandler : IRequestHandler<GetCardQuery, Card>
    {
        private readonly ICardRepository _cardRepository;

        public GetCardQueryHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<Card> Handle(GetCardQuery query, CancellationToken cancellationToken)
        {
            return await _cardRepository.GetAsync(query.Id) ?? throw new CardNotFoundException(query.Id);
        }
    }
}
