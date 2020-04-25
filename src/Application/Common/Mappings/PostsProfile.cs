using Application.Common.Models;
using AutoMapper;
using Domain;

namespace Application.Common.Mappings
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.User != null ?
                        src.User.Name : string.Empty));
        }
    }
}
