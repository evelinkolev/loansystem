using LoanSystem.Application.Abstraction.Generator;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;
using System.Globalization;

namespace LoanSystem.Application.Cards.Commands.CreateCard
{
    internal sealed class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Card>
    {
        private readonly ICardRepository _cardRepository;
        private readonly IStringGenerator _stringGenerator;
        private readonly IPayerRepository _payerRepository;
        private readonly IClock _clock;

        public CreateCardCommandHandler(ICardRepository cardRepository, IStringGenerator stringGenerator, IPayerRepository payerRepository, IClock clock)
        {
            _cardRepository = cardRepository;
            _stringGenerator = stringGenerator;
            _payerRepository = payerRepository;
            _clock = clock;
        }

        public async Task<Card> Handle(CreateCardCommand command, CancellationToken cancellationToken)
        {
            var payer = await _payerRepository.GetAsync(command.PayerId) ?? throw new PayerNotFoundException(command.PayerId);

            var generated16DigitNumber = _stringGenerator.Generate16DigitNumber();
            var cardHolderName = payer.FullName;

            if (command.HolderName != cardHolderName)
            {
                throw new InvalidCardHolderNameException(command.HolderName);
            }

            bool expirationDateValid = DateTime.TryParseExact(
                command.ExpiryDate,
                "MM/dd/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var expirationDate);

            var now = _clock.CurrentDate();

            if (!expirationDateValid || expirationDate.AddMonths(1) < now)
            {
                throw new InvalidCardExpiryDateException(command.ExpiryDate);
            }

            var generated3DigitSecurityCode = _stringGenerator.Generate3DigitSecurityCode();

            var card = new Card
            {
                Number = generated16DigitNumber,
                HolderName = cardHolderName,
                ExpiryDate = expirationDate.ToString(),
                SecurityCode = generated3DigitSecurityCode,
                CreatedDateTime = now,
                PayerId = command.PayerId
            };

            await _cardRepository.CreateAsync(card);
            return card;
        }
    }
}
