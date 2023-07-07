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
        Loan? GetById(Guid id);
        bool Exists(Guid id);
        IEnumerable<Loan> GetAll();
        int Save(Loan loanToSave);
    }
}
