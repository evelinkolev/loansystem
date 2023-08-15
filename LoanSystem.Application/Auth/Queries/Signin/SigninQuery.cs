using LoanSystem.Application.Auth.Common;
using MediatR;

namespace LoanSystem.Application.Auth.Queries.Signin
{
    public record SigninQuery(
        string Email,
        string Password) : IRequest<AuthenticationResult>;

}
