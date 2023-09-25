using LoanSystem.Application.Abstraction.Identity;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using MediatR;
using System.Security.Claims;

namespace LoanSystem.Application.Auth.Queries.Signout
{
    internal sealed class SignoutQueryHandler : IRequestHandler<SignoutQuery, User>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;

        public SignoutQueryHandler(IUserAccessor userAccessor, IUserRepository userRepository)
        {
            _userAccessor = userAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(SignoutQuery query, CancellationToken cancellationToken)
        {
            var userId = _userAccessor.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            return user!;
        }
    }
}
