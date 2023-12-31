﻿using LoanSystem.Application.Abstraction.Persistence;
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

        public async Task<IQueryable<Payment>> FindAllAsync()
        {
            return await Task.FromResult(_payments.Include(x => x.Payer).AsNoTracking());
        }

        public async Task<Payment?> GetAsync(Guid Id)
        {
            return await _payments.Include(x => x.Payer).AsNoTracking().Where(x => x.Id == Id).SingleOrDefaultAsync();
        }
    }
}
