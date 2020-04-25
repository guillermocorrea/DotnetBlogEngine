using Application.Common.Models;
using AutoMapper;
using Domain;

namespace Application.Common.Mappings
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(
                    dest => dest.Role,
                    opt => opt.MapFrom(src => src.Role != null ?
                        src.Role.Name : string.Empty));

        }
    }
}
