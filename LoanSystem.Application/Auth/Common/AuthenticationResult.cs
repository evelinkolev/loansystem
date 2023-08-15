using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Auth.Common
{
    public record AuthenticationResult(
        User User,
        string Token);

}
