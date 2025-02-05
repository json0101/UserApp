using AutoMapper;
using UserApp.Domain.Entities;
using UserApp.Service.Users.Dto;


namespace UserApp.Service.Users.Mapper
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}
