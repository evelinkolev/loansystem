using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.IAuthManager
{
    public interface IAuthManager
    {
        string GenerateToken(User user);
    }
}
