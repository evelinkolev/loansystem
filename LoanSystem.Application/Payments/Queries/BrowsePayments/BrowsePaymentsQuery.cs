using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.BrowsePayments
{
    public record BrowsePaymentsQuery(string? SearchTerm) : IRequest<List<Payment>>;
}
