using LoanSystem.Application.Abstraction.Identity;
using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using MediatR;
using System.Security.Claims;

namespace LoanSystem.Application.Auth.Queries.GetUser
{
    internal sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, User>
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;

        public GetUserQueryHandler(IUserAccessor userAccessor, IUserRepository userRepository)
        {
            _userAccessor = userAccessor;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetUserQuery query, CancellationToken cancellationToken)
        {
            // User cannot access the other user accounts.

            var userId = _userAccessor.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var user = await _userRepository.GetAsync(Guid.Parse(userId));

            return user!;
        }
    }
}
