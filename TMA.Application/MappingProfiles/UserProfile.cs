using AutoMapper;
using TMA.Application.DTOs.Authentication;
using TMA.Domain.Entities;

namespace TMA.Application.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserDto>();
            CreateMap<UserDto, UserEntity>();
        }
    }
}
