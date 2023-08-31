using LoanSystem.Application.Cards.Commands.CreateCard;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Cards.Requests;
using LoanSystem.Contracts.V1.Cards.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class CardController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CardController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost(ApiRoutes.Card.Create)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCardRequest request)
        {
            var command = _mapper.Map<CreateCardCommand>(request);

            var result = await _mediator.Send(command);

            return Ok(_mapper.Map<CardResponse>(result));
        }
    }
}
