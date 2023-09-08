using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.GetPaymentById
{
    internal sealed class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, Payment>
    {
        private readonly IPaymentRepository _paymentRepository;

        public GetPaymentByIdQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> Handle(GetPaymentByIdQuery query, CancellationToken cancellationToken)
        {
            return await _paymentRepository.GetAsync(query.Id) ?? throw new PaymentNotFoundException(query.Id);
        }
    }
}
