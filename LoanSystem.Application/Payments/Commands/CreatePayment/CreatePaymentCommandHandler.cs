using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Payments.Commands.CreatePayment
{
    internal sealed class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPayerRepository _payerRepository;
        private readonly IClock _clock;
        private readonly ICardRepository _cardRepository;

        public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IPayerRepository payerRepository, IClock clock, ICardRepository cardRepository)
        {
            _paymentRepository = paymentRepository;
            _payerRepository = payerRepository;
            _clock = clock;
            _cardRepository = cardRepository;
        }

        public async Task<Payment> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
        {
            var payer = await _payerRepository.GetAsync(command.PayerId) ?? throw new PayerNotFoundException(command.PayerId);

            var findByCondition = await _cardRepository.PayerHaveCardAsync(command.CardId, payer.Id);

            if (!findByCondition)
            {
                throw new NoCardException();
            }

            if (command.Amount > payer.Deposit)
            {
                throw new InsufficientFundsException();
            }

            var now = _clock.CurrentDate();

            payer.Deposit -= command.Amount;
            payer.UpdatedDateTime = now;

            await _payerRepository.UpdateAsync(payer);

            var payment = new Payment
            {
                PayerId = command.PayerId,
                Amount = command.Amount,
                CreatedDateTime = now
            };

            await _paymentRepository.CreateAsync(payment);
            return payment;
        }
    }
}
