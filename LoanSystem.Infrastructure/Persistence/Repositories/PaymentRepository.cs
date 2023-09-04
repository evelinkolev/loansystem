using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Infrastructure.Persistence.Repositories
{
    internal sealed class PaymentRepository : IPaymentRepository
    {
        private readonly LoanSystemContext _context;
        private readonly DbSet<Payment> _payments;

        public PaymentRepository(LoanSystemContext context)
        {
            _context = context;
            _payments = _context.Payments;
        }

        public async Task CreateAsync(Payment payment)
        {
            await _payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }
    }
}
