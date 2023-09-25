using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Auth.Queries.Signout
{
    public record SignoutQuery(Guid Id) : IRequest<User>;
}
