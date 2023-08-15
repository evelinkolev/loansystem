using LoanSystem.Application.Auth.Common;
using MediatR;

namespace LoanSystem.Application.Auth.Commands.Signup
{
    public record SignupCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<AuthenticationResult>;

}
