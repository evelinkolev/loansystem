using LoanSystem.Contracts.V1.Payments.Responses;
using LoanSystem.Models.Domain;
using Mapster;

namespace LoanSystem.Api.Common.Mapping
{
    public class PaymentMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Payment, PaymentResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Amount, src => src.Amount)
                .Map(dest => dest.RequestType, src => src.RequestType)
                .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime)
                .Map(dest => dest.UpdatedDateTime, src => src.UpdatedDateTime)
                .Map(dest => dest.PayerId, src => src.PayerId);
        }
    }
}
