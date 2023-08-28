using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface ICardRepository
    {
        Task CreateAsync(Card card);
    }
}
