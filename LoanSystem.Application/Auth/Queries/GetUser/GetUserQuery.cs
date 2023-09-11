using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Auth.Queries.GetUser
{
    public record GetUserQuery(Guid Id) : IRequest<User>;
}
