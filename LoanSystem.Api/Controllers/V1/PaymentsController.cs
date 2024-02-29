using LoanSystem.Application.Payments.Commands.CreatePayment;
using LoanSystem.Application.Payments.Queries.BrowsePayments;
using LoanSystem.Application.Payments.Queries.GetPayment;
using LoanSystem.Contracts.V1;
using LoanSystem.Contracts.V1.Payments.Requests;
using LoanSystem.Contracts.V1.Payments.Responses;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanSystem.Api.Controllers.V1
{
    [Authorize]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PaymentsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Payments.Browse)]
        public async Task<ActionResult> BrowseAsync([FromQuery] StringParameters parameters)
        {
            var result = await _mediator.Send(new BrowsePaymentsQuery(parameters.SearchTerm, parameters.SortColumn, parameters.SortOrder, parameters.Page, parameters.PageSize));

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Payments.Get)]
        public async Task<ActionResult> GetAsync([FromRoute] Guid paymentId)
        {
            var result = await _mediator.Send(new GetPaymentQuery(paymentId));

            if (result is not null)
            {
                return Ok(_mapper.Map<PaymentResponse>(result));
            }

            return NotFound();
        }

        [HttpPost(ApiRoutes.Payments.Create)]
        public async Task<ActionResult> CreateAsync([FromBody] CreatePaymentRequest request, [FromRoute] Guid payerId, [FromRoute] Guid cardId)
        {
            var command = _mapper.Map<CreatePaymentCommand>((request, payerId, cardId));

            var result = await _mediator.Send(command);

            return Ok(_mapper.Map<PaymentResponse>(result));
        }
    }
}
