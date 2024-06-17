using AutoMapper;
using E_commerce.DTOS;
using E_commerce.Models;

namespace E_commerce.Services
{
    public class MapingProfile : Profile
    {

        public MapingProfile()
        {
            CreateMap<Review, ReviewDto>()
            .ForMember(dest => dest.userName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.userImage, opt => opt.MapFrom(src => src.User.Image))
            .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.User.Id));

            CreateMap<ReviewDto, Review>();
             

        }
    }
}
