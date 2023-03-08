using AutoMapper;
using Chat.Core.DTOs;
using ChatAPI.Entities;

namespace ChatAPI.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>()
                .ForMember
                (
                    dest => dest.Username, 
                    opt => opt.MapFrom(src => src.Username)
                );

            CreateMap<User, UserDTO>()
                .ForMember
                (   
                    dest => dest.Username, 
                    opt => opt.MapFrom(src => src.Username)
                );
        }

    }
}
