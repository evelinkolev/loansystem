using LoanSystem.Contracts.Users.Requests;
using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Borrow;
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
        private readonly IBorrowService _borrowService;
        private readonly UserManager<User> _userManager;

        public UsersController(IAuthorizationService authorizationService,
            IBorrowService borrowService,
            UserManager<User> userManager)
        {
            _authorizationService = authorizationService;
            _borrowService = borrowService;
            _userManager = userManager;
        }

        [HttpGet("api/users/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
        {
            //var loan = _loanRepository.GetById(loanId);

            //if(loan == null)
            //{
            //    return NotFound();
            //}
            return Ok();
            // TODO: Do return operation to return response
        }

        [HttpPost("api/users/join-and-borrow")]
        public async Task<IActionResult> Create([FromBody] BorrowRequest request)
        {
            var borrowResult = _borrowService.Send(request.PurchasePrice,
                request.DownPayment,
                request.LoanTermYears,
                request.InterestRate,
                request.RepaymentDate,
                request.UserId = VerifyUserIdForLoanTakeOut());

            var isAuthorized = await _authorizationService.AuthorizeAsync(User, borrowResult, AuthorizationOperations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

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
