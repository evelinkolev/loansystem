using LoanSystem.Application.Auth.Queries.GetUser;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Auth.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UsersController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Users.Get)]
        public async Task<ActionResult> GetAsync([FromRoute] Guid userId)
        {
            var result = await _mediator.Send(new GetUserQuery(userId));

            if (result is not null)
            {
                return Ok(_mapper.Map<AuthenticationResponse>(result));
            }

            return NotFound();
        }
    }
}
