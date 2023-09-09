using LoanSystem.Application.Payers.Commands.CreatePayer;
using LoanSystem.Application.Payers.Commands.SetupPayerDirectDeposit;
using LoanSystem.Application.Payers.Queries.GetPayerById;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Payers.Requests;
using LoanSystem.Contracts.V1.Payers.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class PayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PayersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Payers.Get)]
        public async Task<ActionResult> GetAsync([FromRoute] Guid payerId)
        {
            var result = await _mediator.Send(new GetPayerByIdQuery(payerId));

            if (result is not null)
            {
                return Ok(_mapper.Map<PayerResponse>(result));
            }

            return NotFound();
        }

        [HttpPost(ApiRoutes.Payers.Create)]
        public async Task<ActionResult> CreateAsync([FromBody]CreatePayerRequest request)
        {
            var command = _mapper.Map<CreatePayerCommand>(request);
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<PayerResponse>(result));
        }

        [HttpPut(ApiRoutes.Payers.Update)]
        public async Task<ActionResult> UpdateAsync([FromBody] SetUpPayerDirectDepositRequest request, [FromRoute] Guid payerId)
        {
            var command = _mapper.Map<SetupPayerDirectDepositCommand>((request, payerId));
            var result = await _mediator.Send(command);
            return Ok(_mapper.Map<PayerResponse>(result));
        }
    }
}
