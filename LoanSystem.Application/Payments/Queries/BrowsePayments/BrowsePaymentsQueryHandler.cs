using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.BrowsePayments
{
    internal sealed class BrowsePaymentsQueryHandler : IRequestHandler<BrowsePaymentsQuery, List<Payment>>
    {
        // To do: make use of dependency injection.
        public Task<List<Payment>> Handle(BrowsePaymentsQuery query, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
