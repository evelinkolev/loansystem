using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.GetPayment
{
    internal sealed class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetAsync(query.Id) ?? throw new PaymentNotFoundException(query.Id);
        }
    }
}
