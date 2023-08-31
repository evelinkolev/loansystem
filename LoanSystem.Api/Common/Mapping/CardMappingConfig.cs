using LoanSystem.Contracts.V1.Cards.Responses;
using LoanSystem.Models.Domain;
using Mapster;

namespace LoanSystem.Api.Common.Mapping
{
    public class CardMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Card, CardResponse>()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Number, src => src.Number)
                .Map(dest => dest.HolderName, src => src.HolderName)
                .Map(dest => dest.ExpiryDate, src => src.ExpiryDate)
                .Map(dest => dest.SecurityCode, src => src.SecurityCode)
                .Map(dest => dest.CreatedDateTime, src => src.CreatedDateTime)
                .Map(dest => dest.UpdatedDateTime, src => src.UpdatedDateTime)
                .Map(dest => dest.PayerId, src => src.PayerId);
        }
    }
}
