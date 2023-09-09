using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Payers.Queries.GetPayerById
{
    internal sealed class GetPayerByIdQueryHandler : IRequestHandler<GetPayerByIdQuery, Payer>
    {
        private readonly IPayerRepository _payerRepository;

        public GetPayerByIdQueryHandler(IPayerRepository payerRepository)
        {
            _payerRepository = payerRepository;
        }

        public async Task<Payer> Handle(GetPayerByIdQuery query, CancellationToken cancellationToken)
        {
            return await _payerRepository.GetAsync(query.Id) ?? throw new PayerNotFoundException(query.Id);
        }
    }
}
