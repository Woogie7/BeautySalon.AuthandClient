using AutoMapper;
using BeautySalon.AuthandClient.Application.DTO;
using BeautySalon.AuthandClient.Domain.Entity;

namespace BeautySalon.AuthandClient.Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegisterUserDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));


        CreateMap<RegisterUserDto, Client>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone));
    }
}