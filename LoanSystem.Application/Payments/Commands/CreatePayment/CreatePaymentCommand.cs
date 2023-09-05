using LoanSystem.Models.Domain;
using MediatR;

namespace LoanSystem.Application.Payments.Commands.CreatePayment
{
    public record CreatePaymentCommand(decimal Amount, Guid PayerId) : IRequest<Payment>;
}
