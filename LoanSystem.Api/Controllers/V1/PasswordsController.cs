using LoanSystem.Application.Auth.Commands.ChangePassword;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Auth.Requests;
using LoanSystem.Contracts.V1.Auth.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class PasswordsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PasswordsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPut(ApiRoutes.Password.Change)]
        public async Task<ActionResult> ChangeAsync([FromBody] ChangePasswordRequest request, [FromRoute] Guid userId)
        {
            var command = _mapper.Map<ChangePasswordCommand>((request, userId));
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<AuthenticationResponse>(result));
        }
    }
}
