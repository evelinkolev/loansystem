using LoanSystem.Application.Abstraction.Auth;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Pwd;
using LoanSystem.Application.Abstraction.Time;
using LoanSystem.Application.Auth.Common;
using LoanSystem.Models.Exceptions;
using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Auth.Commands.Signup
{
    internal sealed class SignupCommandHandler : IRequestHandler<SignupCommand, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IClock _clock;
        private readonly IPasswordHasher _passwordHasher;
        private readonly RegistrationOptions _registrationOptions;

        public SignupCommandHandler(IUserRepository userRepository, IAuthManager authManager, IClock clock, IPasswordHasher passwordHasher, RegistrationOptions registrationOptions)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _clock = clock;
            _passwordHasher = passwordHasher;
            _registrationOptions = registrationOptions;
        }
        public async Task<AuthenticationResult> Handle(SignupCommand command, CancellationToken cancellationToken)
        {
            if (!_registrationOptions.Enabled)
            {
                throw new SignUpDisabledException();
            }

            var email = command.Email.ToLowerInvariant();
            var provider = email.Split("@").Last();
            if (_registrationOptions.InvalidEmailProviders?.Any(x => provider.Contains(x)) is true)
            {
                throw new InvalidEmailException(email);
            }

            if (string.IsNullOrWhiteSpace(command.Password) || command.Password.Length is > 100 or < 6)
            {
                throw new InvalidPasswordException("not matching the criteria");
            }

            var user = await _userRepository.GetAsync(email);
            if (user is not null)
            {
                throw new EmailInUseException();
            }

            var now = _clock.CurrentDate();

            _passwordHasher.CreatePasswordHash(command.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                CreatedDateTime = now
            };

            await _userRepository.AddAsync(user);

            var token = _authManager.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
