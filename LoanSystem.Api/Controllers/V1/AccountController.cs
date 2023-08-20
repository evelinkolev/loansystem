using LoanSystem.Application.Auth.Commands.Signup;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Auth.Requests;
using LoanSystem.Contracts.V1.Auth.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Account.Signup)]
        public async Task<ActionResult> SignupAsync([FromBody] SignupRequest request)
        {
            var command = _mapper.Map<SignupCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }
    }
}
