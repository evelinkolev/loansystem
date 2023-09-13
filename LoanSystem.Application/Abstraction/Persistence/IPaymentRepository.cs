using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface IPaymentRepository
    {
        Task<IQueryable<Payment>> FindAllAsync();
        Task CreateAsync(Payment payment);
        Task<Payment?> GetAsync(Guid Id);
    }
}
