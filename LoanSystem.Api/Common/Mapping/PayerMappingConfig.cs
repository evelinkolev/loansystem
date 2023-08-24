using LoanSystem.Contracts.V1.Payers.Responses;
using LoanSystem.Models.Domain;
using Mapster;

namespace LoanSystem.Api.Common.Mapping
{
    public class PayerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payer, PayerResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.FullName, src => src.FullName)
                .Map(dest => dest.Deposit, src => src.Deposit)
                .Map(dest => dest.RoutingNumber, src => src.RoutingNumber)
                .Map(dest => dest.AccountNumber, src => src.AccountNumber)
                .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime)
                .Map(dest => dest.UserId, src => src.UserId);
        }
    }
}
