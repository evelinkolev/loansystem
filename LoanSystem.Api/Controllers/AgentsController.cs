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

            if(loan.State == State.Approved) // 1 True // 2 False
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
    }
}