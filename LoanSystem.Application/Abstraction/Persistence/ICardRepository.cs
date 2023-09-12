using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface ICardRepository
    {
        Task CreateAsync(Card card);
        Task<Card?> GetAsync(Guid id);
        Task DeleteAsync(Card card);
        Task<bool> PayerHaveCardAsync(Guid cardId, Guid payerId);
    }
}
