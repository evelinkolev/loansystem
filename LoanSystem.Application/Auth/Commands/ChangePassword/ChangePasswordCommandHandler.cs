using LoanSystem.Application.Abstraction.Auth;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Pwd;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Application.Auth.Common;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Auth.Commands.ChangePassword
{
    internal sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthManager _authManager;
        private readonly IClock _clock;

        public ChangePasswordCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IAuthManager authManager, IClock clock)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _authManager = authManager;
            _clock = clock;
        }

        public async Task<AuthenticationResult> Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(command.UserId) ?? throw new UserNotFoundException(command.UserId);

            if(!_passwordHasher.VerifyPasswordHash(command.CurrentPassword, user.PasswordHash!, user.PasswordSalt!))
            {
                throw new InvalidPasswordException("current password is invalid");
            }

            _passwordHasher.CreatePasswordHash(command.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            var now = _clock.CurrentDate();

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.UpdatedDateTime = now;

            var token = _authManager.GenerateToken(user);

            await _userRepository.UpdateAsync(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
