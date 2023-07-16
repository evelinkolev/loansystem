using LoanSystem.Contracts.Authorization.Responses;
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
    public class AgentsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoanRepository _loanRepository;

        public AgentsController(IAuthorizationService authorizationService,
            ILoanRepository loanRepository)
        {
            _authorizationService = authorizationService;
            _loanRepository = loanRepository;
        }

        [HttpGet("api/agents/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
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

        [HttpPut("api/agents/join-and-borrow/{loanId}")]
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

            loan.State = State.Approved;

            var canApprove = await _authorizationService.AuthorizeAsync(User, loan, AuthorizationOperations.Approve);

            if (!canApprove.Succeeded)
            {
                loan.State = State.Submitted;
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
                Status = (int)loan.State,
                UserId = loan.UserId
            };

            return Ok(response);
        }
    }
}