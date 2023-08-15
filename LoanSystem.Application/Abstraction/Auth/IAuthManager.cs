using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Auth
{
    public interface IAuthManager
    {
        string GenerateToken(User user);
    }
}
