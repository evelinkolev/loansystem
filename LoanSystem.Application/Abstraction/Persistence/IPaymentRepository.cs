using LoanSystem.Models.Domain;

namespace LoanSystem.Application.Abstraction.Persistence
{
    public interface IPaymentRepository
    {
        Task CreateAsync(Payment payment);
        Task<Payment?> GetAsync(Guid Id);
    }
}
