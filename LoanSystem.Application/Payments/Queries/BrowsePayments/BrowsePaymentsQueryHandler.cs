using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.BrowsePayments
{
    internal sealed class BrowsePaymentsQueryHandler : IRequestHandler<BrowsePaymentsQuery, List<Payment>>
    {
        private readonly IPaymentRepository _paymentRepository;

        public BrowsePaymentsQueryHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<Payment>> Handle(BrowsePaymentsQuery query, CancellationToken cancellationToken)
        {
            var paymentsQuery = await _paymentRepository.FindAllAsync();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                paymentsQuery = paymentsQuery.Where(x => x.Amount.ToString().Contains(query.SearchTerm)
                    || x.RequestType.Contains(query.SearchTerm));
            }

            return paymentsQuery.ToList();
        }
    }
}
