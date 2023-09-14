using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using MediatR;
using System.Linq.Expressions;

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

            // Filtering
            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                paymentsQuery = paymentsQuery.Where(x => x.Amount.ToString().Contains(query.SearchTerm)
                    || x.RequestType.Contains(query.SearchTerm));
            }

            // Sorting
            if (query.SortOrder?.ToLower() == "desc")
            {
                paymentsQuery = paymentsQuery.OrderByDescending(GetSortProperty(query));
            }
            else
            {
                paymentsQuery = paymentsQuery.OrderBy(GetSortProperty(query));
            }

            return paymentsQuery.ToList();
        }
        private static Expression<Func<Payment, object>> GetSortProperty(BrowsePaymentsQuery query) =>
            query.SortColumn?.ToLower() switch
            {
                "date" => payment => payment.CreatedDateTime,
                "amount" => payment => payment.Amount,
                "type" => payment => payment.RequestType,
                _ => payment => payment.Id
            };
    }
}
