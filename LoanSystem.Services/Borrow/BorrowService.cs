using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanSystem.Services.Borrow
{
    public class BorrowService : IBorrowService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;

        public BorrowService(ILoanRepository loanRepository, IAuthorizationService authorizationService, UserManager<User> userManager)
        {
            _loanRepository = loanRepository;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public BorrowResult Send(decimal purchasePrice, decimal downPayment, int termInYears, decimal interestRate, DateTime dateTime)
        {
            if(purchasePrice < 0 || purchasePrice > 300000)
            {
                return new BorrowResult
                {
                    Messages = new[] { "Invalid Purchase Price." }
                };
            }

            if (downPayment < 0 || downPayment > purchasePrice)
            {
                return new BorrowResult
                {
                    Messages = new[] { "Invalid Down Payment." }
                };
            }

            if (termInYears < 0 || termInYears > 50)
            {
                return new BorrowResult
                {
                    Messages = new[] { "Invalid Term." }
                };
            }

            var now = DateTime.UtcNow;
            if (dateTime < now)
            {
                return new BorrowResult
                {
                    Messages = new[] { "Cant't create contract with later date." }
                };
            }

            return null;
        }
    }
}
