using AutoMapper;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Service.Services.Users
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;
        IMapper _mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public UserDto? GetUserByEmail(string email)
        {
            var user = _userRepository.GetDbSet().Where(x => x.Email == email && x.Active).FirstOrDefault();
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
        public UserDto? GetUserByEmployeeCod(string employeeCod)
        {
            var user = _userRepository.GetDbSet().Where(x => x.EmployeeCode == employeeCod && x.Active).FirstOrDefault();

            if (user == null)
            {
                throw new Exception();
            }
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }
    }
}
