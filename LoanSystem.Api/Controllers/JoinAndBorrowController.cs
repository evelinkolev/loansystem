using LoanSystem.Contracts.Users.Requests;
using LoanSystem.Contracts.Users.Response;
using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class JoinAndBorrowController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoanRepository _loanRepository;
        private readonly UserManager<User> _userManager;

        public JoinAndBorrowController(IAuthorizationService authorizationService, ILoanRepository loanRepository, UserManager<User> userManager)
        {
            _authorizationService = authorizationService;
            _loanRepository = loanRepository;
            _userManager = userManager;
        }

        [HttpGet("api/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
        {
            var loan = _loanRepository.GetById(loanId);

            if (loan == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.LoanAgentsRole) ||
                User.IsInRole(Constants.LoanAdministratorsRole);

            var currentUserId = VerifyUserIdForLoanTakeOut();

            if (!isAuthorized && currentUserId != loan.UserId && loan.State != State.Approved)
            {
                return Forbid();
            }

            var response = new LoanResponse
            {
                Id = loan.Id,
                PurchasePrice = loan.PurchasePrice,
                DownPayment = loan.DownPayment,
                LoanTermYears = loan.LoanTermYears,
                InterestRate = loan.InterestRate,
                RepaymentDate = loan.RepaymentDate,
                UserId = loan.UserId,
                Status = (int)loan.State
            };

            return Ok(response);
        }

        [HttpPost("api/join-and-borrow")]
        public async Task<IActionResult> Create([FromBody] LoanRequest request)
        {
            var loan = new Loan
            {
                PurchasePrice = request.PurchasePrice,
                DownPayment = request.DownPayment,
                LoanTermYears = request.LoanTermYears,
                InterestRate = request.InterestRate,
                RepaymentDate = request.RepaymentDate,
                UserId = VerifyUserIdForLoanTakeOut()
            };

            loan.State = State.Submitted;

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _loanRepository.Save(loan);

            var response = new LoanResponse
            {
                Id = loan.Id,
                PurchasePrice = loan.PurchasePrice,
                DownPayment = loan.DownPayment,
                LoanTermYears = loan.LoanTermYears,
                InterestRate = loan.InterestRate,
                RepaymentDate = loan.RepaymentDate,
                UserId = loan.UserId,
                Status = (int)loan.State
            };

            return Ok(response);
        }

        [HttpGet("api/join-and-borrow")]
        public IActionResult GetAll()
        {
            var loans = _loanRepository.GetAll();

            var isAuthorized = User.IsInRole(Constants.LoanAgentsRole) ||
                User.IsInRole(Constants.LoanAdministratorsRole);

            var currentUserId = VerifyUserIdForLoanTakeOut();

            if (!isAuthorized)
            {
                loans = loans.Where(loan => loan.UserId == currentUserId);
            }

            var loanResponse = loans.Select(loans => new LoanResponse
            {
                Id = loans.Id,
                PurchasePrice = loans.PurchasePrice,
                DownPayment = loans.DownPayment,
                LoanTermYears = loans.LoanTermYears,
                InterestRate = loans.InterestRate,
                RepaymentDate = loans.RepaymentDate,
                UserId = loans.UserId,
                Status = (int)loans.State
            }).ToList();

            return Ok(loanResponse);
        }

        [HttpPut("api/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Update([FromRoute] Guid loanId)
        {
            var loan = _loanRepository.GetById(loanId);

            if (loan == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            if (loan.State == State.Approved) // 1 True // 2 False
            {
                var canApprove = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Approve); // 1 True

                if (!canApprove.Succeeded) // 1 True
                {
                    loan.State = State.Submitted;
                }
            }
            else // 2 Trigger
            {
                loan.State = State.Approved;
            }

            _loanRepository.Save(loan); // 1 Trigger // 2 Trigger

            var response = new LoanResponse
            {
                Id = loan.Id,
                PurchasePrice = loan.PurchasePrice,
                DownPayment = loan.DownPayment,
                LoanTermYears = loan.LoanTermYears,
                InterestRate = loan.InterestRate,
                RepaymentDate = loan.RepaymentDate,
                Status = (int)loan.State,
                UserId = loan.UserId
            };

            return Ok(response);
        }

        [HttpDelete("api/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Delete([FromRoute] Guid loanId)
        {
            var loan = _loanRepository.GetById(loanId);

            if (loan == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            loan.State = State.Rejected;

            _loanRepository.Save(loan);

            return NoContent();
        }

        private string VerifyUserIdForLoanTakeOut()
        {
            var value = _userManager.GetUserId(User);

            if (value == null)
            {
                return string.Empty;
            }

            return value;
        }
    }
}
