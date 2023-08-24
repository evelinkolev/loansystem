using LoanSystem.Application.Abstraction.Generator;
using LoanSystem.Application.Abstraction.Identity;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace LoanSystem.Application.Payers.Commands
{
    internal sealed class CreatePayerCommandHandler : IRequestHandler<CreatePayerCommand, Payer>
    {
        private readonly IPayerRepository _payerRepository;
        private readonly IClock _clock;
        private readonly IStringGenerator _stringGenerator;
        private readonly IUserAccessor _userAccessor;

        public CreatePayerCommandHandler(IPayerRepository payerRepository, IClock clock, IStringGenerator stringGenerator, IUserAccessor userAccessor)
        {
            _payerRepository = payerRepository;
            _clock = clock;
            _stringGenerator = stringGenerator;
            _userAccessor = userAccessor;
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

            //var isAuthenticated = _userAccessor.User.Identity.IsAuthenticated;

            var userId = _userAccessor.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var generated8DiditRoutingNumber = _stringGenerator.Generate8DigitRoutingNumber();
            var generated9DigitAccountNumber = _stringGenerator.Generate9DigitAccountNumber();

            var now = _clock.CurrentDate();

            var payer = new Payer
            {
                FullName = command.FullName,
                Deposit = command.Deposit,
                RoutingNumber = generated8DiditRoutingNumber,
                AccountNumber = generated9DigitAccountNumber,
                UserId = Guid.Parse(userId),
                CreatedDateTime = now,
            };

            await _payerRepository.CreateAsync(payer);
            return payer;
        }
    }
}
