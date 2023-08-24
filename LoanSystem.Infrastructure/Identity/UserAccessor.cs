using LoanSystem.Application.Abstraction.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace LoanSystem.Infrastructure.Identity
{
    public partial class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }
        public ClaimsPrincipal User => _accessor.HttpContext!.User;
    }
}
