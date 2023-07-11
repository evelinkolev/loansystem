using LoanSystem.Contracts.Users.Requests;
using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    [Authorize(Policy)]
    public class UsersController : ControllerBase
    {
        private const string Policy = "Users";
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoanRepository _loanRepository;
        private readonly UserManager<User> _userManager;

        public UsersController(IAuthorizationService authorizationService,
            ILoanRepository loanRepository,
            UserManager<User> userManager)
        {
            _authorizationService = authorizationService;
            _loanRepository = loanRepository;
            _userManager = userManager;
        }

        [HttpGet("api/users/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
        {
            var loan = _loanRepository.GetById(loanId);

            if(loan == null)
            {
                return NotFound();
            }
            return Ok();
            // TODO: Do return operation to return response
        }

        [HttpPost("api/users/join-and-borrow")]
        public async Task<IActionResult> Create([FromBody] BorrowRequest request)
        {
            var loan = new Loan
            {
                PurchasePrice = request.PurchasePrice,
                DownPayment = request.DownPayment,
                LoanTermYears = request.LoanTermYears,
                InterestRate = request.InterestRate,
                RepaymentDate = request.RepaymentDate,
                UserId = GetUserId()
            };

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            _loanRepository.Save(loan);
            return NoContent();
        }

        private string GetUserId()
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
