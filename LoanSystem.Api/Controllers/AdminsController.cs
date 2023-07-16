using LoanSystem.Contracts.Authorization.Responses;
using LoanSystem.Data.Repositories;
using LoanSystem.Models.Domain;
using LoanSystem.Services.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AdminsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ILoanRepository _loanRepository;

        public AdminsController(IAuthorizationService authorizationService,
            ILoanRepository loanRepository)
        {
            _authorizationService = authorizationService;
            _loanRepository = loanRepository;
        }

        [HttpGet("api/admins/join-and-borrow/{loanId}")]
        public async Task<IActionResult> Get([FromRoute] Guid loanId)
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

            return Ok(new LoanContextResponse { Loan = loan });
        }

        [HttpDelete("api/admins/join-and-borrow/{loanId}")]
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
    }
}
