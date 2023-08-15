using LoanSystem.Application.Auth.Common;
using MediatR;

namespace LoanSystem.Application.Auth.Commands.Signup
{
    internal sealed class SignupCommandHandler : IRequestHandler<SignupCommand, AuthenticationResult>
    {
        public Task<AuthenticationResult> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
