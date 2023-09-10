using LoanSystem.Application.Abstraction.Persistence;
using LoanSystem.Models.Domain;
using LoanSystem.Models.Exceptions;
using MediatR;

namespace LoanSystem.Application.Payers.Queries.GetPayer
{
    internal sealed class GetPayerQueryHandler : IRequestHandler<GetPayerQuery, Payer>
    {
        private readonly IPayerRepository _payerRepository;

        public GetPayerQueryHandler(IPayerRepository payerRepository)
        {
            _payerRepository = payerRepository;
        }

        public async Task<Payer> Handle(GetPayerQuery query, CancellationToken cancellationToken)
        {
            return await _payerRepository.GetAsync(query.Id) ?? throw new PayerNotFoundException(query.Id);
        }
    }
}
