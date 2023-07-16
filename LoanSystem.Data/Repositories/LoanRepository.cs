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

        public bool Exists(Guid id)
        {
            return _context.Loan.Any(x => x.Id == id);
        }

        public IEnumerable<Loan> GetAll()
        {
            return _context.Loan.AsNoTracking().ToList();
        }

        public Loan? GetById(Guid id)
        {
            return _context.Loan.Find(id);
        }

        public int Save(Loan loanToSave)
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

            int numOfEntriesWritten = _context.SaveChanges();
            return numOfEntriesWritten;
        }
    }
}
