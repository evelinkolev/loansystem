using AutoMapper;
using LoanSystem.Api.DTO;
using LoanSystem.Models.Domain;

namespace LoanSystem.Api.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDto, User>()
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
