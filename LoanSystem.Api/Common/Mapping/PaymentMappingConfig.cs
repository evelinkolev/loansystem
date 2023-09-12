using LoanSystem.Application.Payments.Commands.CreatePayment;
using LoanSystem.Contracts.V1.Payments.Requests;
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

            config.NewConfig<(CreatePaymentRequest Request, Guid PayerId, Guid CardId), CreatePaymentCommand>()
                .Map(dest => dest.PayerId, src => src.PayerId)
                .Map(dest => dest.CardId, src => src.CardId)
                .Map(dest => dest, src => src.Request);
        }
    }
}
