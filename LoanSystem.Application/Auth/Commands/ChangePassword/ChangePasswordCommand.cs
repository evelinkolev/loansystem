using LoanSystem.Application.Auth.Common;
using MediatR;

namespace LoanSystem.Application.Auth.Commands.ChangePassword
{
    public record ChangePasswordCommand(Guid UserId, string CurrentPassword, string NewPassword)
        :IRequest<AuthenticationResult>;
}
