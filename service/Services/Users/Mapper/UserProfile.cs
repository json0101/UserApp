using AutoMapper;
using UserApp.Domain.Entities;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Service.Services.Users.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
