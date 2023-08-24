using System.Security.Claims;

namespace LoanSystem.Application.Abstraction.Identity
{
    public interface IUserAccessor
    {
        ClaimsPrincipal User { get; }
    }
}
