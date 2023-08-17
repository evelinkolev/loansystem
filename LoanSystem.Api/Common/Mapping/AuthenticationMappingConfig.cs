using LoanSystem.Application.Auth.Common;
using LoanSystem.Contracts.V1.Auth.Responses;
using Mapster;

namespace LoanSystem.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest, src => src.User);
        }
    }
}
