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
        Loan? GetById(int id);
        bool Exists(int id);
        IEnumerable<Loan> GetAll();
        int Save(Loan loanToSave);
    }
}
