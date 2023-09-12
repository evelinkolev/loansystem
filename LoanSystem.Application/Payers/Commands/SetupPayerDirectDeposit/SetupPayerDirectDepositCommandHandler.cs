using LoanSystem.Application.Abstraction.Identity;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;
using System.Security.Claims;

namespace LoanSystem.Application.Payers.Commands.SetupPayerDirectDeposit
{
    internal sealed class SetupPayerDirectDepositCommandHandler :
        IRequestHandler<SetupPayerDirectDepositCommand, Payer>
    {
        private readonly IPayerRepository _payerRepository;
        private readonly IUserAccessor _userAccessor;
        private readonly IClock _clock;

        public SetupPayerDirectDepositCommandHandler(IPayerRepository payerRepository, IUserAccessor userAccessor, IClock clock)
        {
            _payerRepository = payerRepository;
            _userAccessor = userAccessor;
            _clock = clock;
        }

        public async Task<Payer> Handle(SetupPayerDirectDepositCommand command, CancellationToken cancellationToken)
        {
            var payer = await _payerRepository.GetAsync(command.PayerId) ?? throw new PayerNotFoundException(command.PayerId);

            var userId = _userAccessor.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var findByCondition = await _payerRepository.UserHavePayerAsync(payer.Id, Guid.Parse(userId));

            if (!findByCondition)
            {
                throw new NoPayerException();
            }

            if (command.RoutingNumber != payer.RoutingNumber)
            {
                throw new InvalidRoutingNumberException(command.RoutingNumber);
            }

            if (command.AccountNumber != payer.AccountNumber)
            {
                throw new InvalidAccountNumberException(command.AccountNumber);
            }

            decimal sum = payer.Deposit + command.Deposit;

            if (sum > decimal.MaxValue || command.Deposit < 0)
            {
                throw new PayerArgumentException(command.Deposit);
            }

            var now = _clock.CurrentDate();

            payer.Deposit += command.Deposit;
            payer.UpdatedDateTime = now;

            await _payerRepository.UpdateAsync(payer);

            return payer;
        }
    }
}
