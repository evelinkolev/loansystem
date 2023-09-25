using LoanSystem.Application.Auth.Commands.Signup;
using LoanSystem.Application.Auth.Queries.GetUser;
using LoanSystem.Application.Auth.Queries.Signin;
using LoanSystem.Application.Auth.Queries.Signout;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Auth.Requests;
using LoanSystem.Contracts.V1.Auth.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    //[AllowAnonymous]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private const string AccessTokenCookie = "__access-token";

        public AccountsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Accounts.Signup)]
        public async Task<ActionResult> SignupAsync([FromBody] SignupRequest request)
        {
            var command = _mapper.Map<SignupCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }

        [HttpPost(ApiRoutes.Accounts.Signin)]
        public async Task<ActionResult> SigninAsync([FromBody] SigninRequest request)
        {
            var query = _mapper.Map<SigninQuery>(request);
            var result = await _mediator.Send(query);
            Response.Cookies.Append(AccessTokenCookie, result.Token, new CookieOptions
            {
                HttpOnly = true
            });
            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }

        [Authorize]
        [HttpDelete(ApiRoutes.Accounts.Signout)]
        public async Task<ActionResult> SignoutAsync([FromBody] SignoutRequest request)
        {
            var result = await _mediator.Send(new SignoutQuery(request.Id));

            Response.Cookies.Delete(AccessTokenCookie, new CookieOptions
            {
                HttpOnly = true
            });

            return NotFound();
        }
    }
}
