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
        public BorrowService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;

        }
        public BorrowResult Send(decimal purchasePrice, decimal downPayment, int termInYears, decimal interestRate, DateTime dateTime, string userId)
        {
            var loanParameters = new Loan
            {
                PurchasePrice = purchasePrice,
                DownPayment = downPayment,
                LoanTermYears = termInYears,
                InterestRate = interestRate,
                RepaymentDate = dateTime,
                UserId = userId              
            };

            var validation = ValidateBorrowResultForLoanFormalParameters(loanParameters);
            if (!validation.Succes)
            {
                return new BorrowResult
                {
                    Messages = validation.Messages.Select(m => m.ToString())
                };
            }

            _loanRepository.Save(loanParameters);

            return new BorrowResult
            {
                Succes = true,
                Messages = new[] { "Succes." }
            };
        }
        private BorrowResult ValidateBorrowResultForLoanFormalParameters(Loan loanToValidate)
        {
            if (loanToValidate.PurchasePrice < 0 || loanToValidate.PurchasePrice > 300000)
            {
                return new BorrowResult
                {
                    Succes = false,
                    Messages = new[] { "Invalid Purchase Price." }
                };
            }

            if (loanToValidate.DownPayment < 0 || loanToValidate.DownPayment > loanToValidate.PurchasePrice)
            {
                return new BorrowResult
                {
                    Succes = false,
                    Messages = new[] { "Invalid Down Payment." }
                };
            }

            if (loanToValidate.LoanTermYears < 0 || loanToValidate.LoanTermYears > 50)
            {
                return new BorrowResult
                {
                    Succes = false,
                    Messages = new[] { "Invalid Term." }
                };
            }

            var now = DateTime.UtcNow;
            if (loanToValidate.RepaymentDate < now)
            {
                return new BorrowResult
                {
                    Succes = false,
                    Messages = new[] { "Error occurred to the date of signature of the contract." }
                };
            }

            if (loanToValidate.UserId == null)
            {
                return new BorrowResult
                {
                    Succes = false,
                    Messages = new[] { "Identity error." }
                };
            }

            return new BorrowResult
            {
                Succes = true,
                Messages = new[] { "Formal parameters successfully passed validation." }
            };
        }
        public decimal ReturnEquatedMonthlyInstallment(Loan loan)
        {
            decimal payment = 0;

            if (loan.LoanTermMonths > 0)
            {
                if (loan.InterestRate != 0)
                {
                    decimal rate = loan.InterestRate / 12 / 100;

                    decimal factor = rate + (rate / (DecPow(rate + 1m, loan.LoanTermMonths) - 1m));

                    payment = loan.LoanAmount * factor;
                }
                else payment = loan.LoanAmount / loan.LoanTermMonths;
            }
            return Math.Round(payment, 2);
        }
        private decimal DecPow(decimal x, decimal y) => (decimal)Math.Pow((double)x, (double)y);
    }
}
