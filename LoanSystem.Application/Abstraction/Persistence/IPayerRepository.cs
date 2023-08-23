using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface IPayerRepository
    {
        Task CreateAsync(Payer payer);
    }
}
