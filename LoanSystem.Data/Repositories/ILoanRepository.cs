using LoanSystem.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Data.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<int> SaveAsync(Loan loanToSave);
    }
}
