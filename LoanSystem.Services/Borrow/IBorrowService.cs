using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace LoanSystem.Services.Borrow
{
    public interface IBorrowService
    {
        BorrowResult Send(decimal purchasePrice, decimal downPayment, int termInYears, decimal interestRate, DateTime dateTime);
    }
}