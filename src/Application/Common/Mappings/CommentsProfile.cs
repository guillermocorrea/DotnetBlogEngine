using Application.Common.Models;
using AutoMapper;
using Domain;

namespace Application.Common.Mappings
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comment, CommentDto>();
        }
    }
}
