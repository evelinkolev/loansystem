using Azure.Core;
using LoanSystem.Contracts.Authorization.Responses;
using LoanSystem.Contracts.Users.Requests;
using LoanSystem.Contracts.Users.Response;
using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Borrow;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsersController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoanRepository _loanRepository;
        private readonly UserManager<User> _userManager;

        public UsersController(IAuthorizationService authorizationService,
            UserManager<User> userManager,
            ILoanRepository loanRepository)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _loanRepository = loanRepository;
        }

        [HttpGet("api/users/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
        {
            var loan = _loanRepository.GetById(loanId);

            if(loan == null)
            {
                return NotFound();
            }

            if (!User.Identity!.IsAuthenticated)
            {
                return Challenge();
            }

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Read);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return Ok(new LoanContextResponse { Loan = loan });
        }

        [HttpPost("api/users/join-and-borrow")]
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

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _loanRepository.Save(loan);

            var response = new LoanResponse
            {
                Id = loan.Id,
                PurchasePrice = request.PurchasePrice,
                DownPayment = request.DownPayment,
                LoanTermYears = request.LoanTermYears,
                InterestRate = request.InterestRate,
                RepaymentDate = request.RepaymentDate,
                UserId = loan.UserId
            };

            return Ok(response);
        }

        [HttpGet("api/users/join-and-borrow")]
        public async Task<IActionResult> GetAll()
        {
            var loans = _loanRepository.GetAll();

            var isAuthorized = User.IsInRole(Constants.LoanAgentsRole) ||
                User.IsInRole(Constants.LoanAdministratorsRole);

            var currentUserId = VerifyUserIdForLoanTakeOut();

            if (!isAuthorized)
            {
                loans = loans.Where(loan => loan.State == State.Approved || loan.UserId == currentUserId);
            }

            var loanResponse = loans.Select(loans => new LoanResponse
            {
                Id = loans.Id,
                DownPayment = loans.DownPayment,
                LoanTermYears = loans.LoanTermYears,
                InterestRate = loans.InterestRate,
                RepaymentDate = loans.RepaymentDate,
                UserId = loans.UserId
            }).ToList();

            return Ok(loanResponse);
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
