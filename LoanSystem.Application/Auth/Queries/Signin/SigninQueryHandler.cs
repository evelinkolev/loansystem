using LoanSystem.Application.Auth.Common;
using MediatR;

namespace LoanSystem.Application.Auth.Queries.Signin
{
    internal sealed class SigninQueryHandler : IRequestHandler<SigninQuery, AuthenticationResult>
    {
        public Task<AuthenticationResult> Handle(SigninQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
