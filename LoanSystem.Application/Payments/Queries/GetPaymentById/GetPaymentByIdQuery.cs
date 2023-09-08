using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Queries.GetPaymentById
{
    public record GetPaymentByIdQuery(Guid Id) : IRequest<Payment>;
}
