using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(string email);
        Task AddAsync(User user);
        Task <User?> GetAsync(Guid id);
        Task UpdateAsync(User user);
    }
}
