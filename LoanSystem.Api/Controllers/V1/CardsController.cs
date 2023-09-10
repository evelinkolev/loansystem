using LoanSystem.Application.Cards.Commands.CreateCard;
using LoanSystem.Application.Cards.Commands.DeleteCard;
using LoanSystem.Application.Cards.Queries.GetCard;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Cards.Requests;
using LoanSystem.Contracts.V1.Cards.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class CardsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CardsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Cards.Get)]
        public async Task<ActionResult> GetAsync([FromRoute] Guid cardId)
        {
            var result = await _mediator.Send(new GetCardQuery(cardId));

            if (result is not null)
            {
                return Ok(_mapper.Map<CardResponse>(result));
            }

            return NotFound();
        }

        [HttpPost(ApiRoutes.Cards.Create)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateCardRequest request)
        {
            var command = _mapper.Map<CreateCardCommand>(request);

            var result = await _mediator.Send(command);

            return Ok(_mapper.Map<CardResponse>(result));
        }

        [HttpDelete(ApiRoutes.Cards.Delete)]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid cardId)
        {
            var result = await _mediator.Send(new DeleteCardCommand(cardId));

            if (!result) { return NotFound(); }

            return NoContent();
        }
    }
}
