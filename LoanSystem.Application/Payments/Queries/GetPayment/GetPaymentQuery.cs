using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.GetPayment
{
    public record GetPaymentQuery(Guid Id) : IRequest<Payment>;
}
