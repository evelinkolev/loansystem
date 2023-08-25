using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanSystem.Infrastructure.Persistence.Repositories
{
    internal sealed class PayerRepository : IPayerRepository
    {
        private readonly LoanSystemContext _context;
        private readonly DbSet<Payer> _payers;

        public PayerRepository(LoanSystemContext context)
        {
            _context = context;
            _payers = _context.Payers;
        }
        public async Task CreateAsync(Payer payer)
        {
            await _payers.AddAsync(payer);
            await _context.SaveChangesAsync();
        }

        public async Task<Payer?> GetAsync(Guid id)
        {
            return await _payers.AsNoTracking().Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task SetUpPayerDirectDepositAsync(Payer payer)
        {
            _payers.Update(payer);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserHavePayerAsync(Guid payerId, Guid userId)
        {
            var payer = await GetAsync(payerId);

            if (payer is null)
            {
                return false;
            }

            if (payer.UserId != userId)
            {
                return false;
            }

            return true;
        }
    }
}
