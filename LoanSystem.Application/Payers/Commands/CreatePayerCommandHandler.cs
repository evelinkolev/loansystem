using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;
using System.Text.RegularExpressions;

namespace LoanSystem.Application.Payers.Commands
{
    internal sealed class CreatePayerCommandHandler : IRequestHandler<CreatePayerCommand, Payer>
    {
        private readonly IPayerRepository _payerRepository;
        private readonly IClock _clock;

        public CreatePayerCommandHandler(IPayerRepository payerRepository, IClock clock)
        {
            _payerRepository = payerRepository;
            _clock = clock;
        }

        public async Task<Payer> Handle(CreatePayerCommand command, CancellationToken cancellationToken)
        {
            var regex = new Regex("^([a-zA-Z]*)\\s+([a-zA-Z ]*)$");

            if (string.IsNullOrEmpty(command.FullName) || !regex.IsMatch(command.FullName))
            {
                throw new PayerArgumentException(command.FullName);
            }

            if (command.Deposit is > decimal.MaxValue or < 0)
            {
                throw new PayerArgumentException(command.Deposit);
            }

            var now = _clock.CurrentDate();

            var payer = new Payer
            {
                FullName = command.FullName,
                Deposit = command.Deposit,
                CreatedDateTime = now,
            };

            await _payerRepository.CreateAsync(payer);
            return payer;
        }
    }
}
