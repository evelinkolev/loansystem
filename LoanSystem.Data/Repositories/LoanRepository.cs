using LoanSystem.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Data.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LoanSystemContext _context;

        public LoanRepository(LoanSystemContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Loan.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loan.AsNoTracking().ToListAsync();
        }

        public async Task<Loan?> GetByIdAsync(Guid id)
        {
            return await _context.Loan.FindAsync(id);
        }

        public async Task<int> SaveAsync(Loan loanToSave)
        {
            if (loanToSave.State == State.Submitted)
            {
                _context.Add(loanToSave);
            }
            else if (loanToSave.State == State.Rejected)
            {
                _context.Remove(loanToSave);
            }
            else if (loanToSave.State == State.Approved)
            {
                _context.Update(loanToSave);
            }

            int numOfEntriesWritten = await _context.SaveChangesAsync();
            return numOfEntriesWritten;
        }
    }
}
