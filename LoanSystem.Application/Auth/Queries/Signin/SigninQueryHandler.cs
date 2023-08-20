using LoanSystem.Application.Abstraction.Auth;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Application.Abstraction.Pwd;
using LoanSystem.Application.Auth.Common;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Auth.Queries.Signin
{
    internal sealed class SigninQueryHandler : IRequestHandler<SigninQuery, AuthenticationResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthManager _authManager;
        private readonly IPasswordHasher _passwordHasher;

        public SigninQueryHandler(IUserRepository userRepository, IAuthManager authManager, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _authManager = authManager;
            _passwordHasher = passwordHasher;
        }
        public async Task<AuthenticationResult> Handle(SigninQuery query, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(query.Email.ToLowerInvariant()) ?? throw new InvalidCredentialsException();

            if (!_passwordHasher.VerifyPasswordHash(query.Password, user.PasswordHash!, user.PasswordSalt!))
            {
                throw new InvalidCredentialsException();
            }

            var token = _authManager.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}
