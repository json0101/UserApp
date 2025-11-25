using AutoMapper;
using service.Commons.Exceptions;
using System.Security.Cryptography;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Users.Dto;
using Microsoft.AspNetCore.Http;

namespace UserApp.Service.Services.Users
{
    public class UserService : IUserService
    {
        IRepository<User> _userRepository;
        IMapper _mapper;
        IHttpContextAccessor _httpContextAccessor;
        public UserService(
            IRepository<User> userRepository, 
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public void ValidateUser(CreateUserDto createUser)
        {
            var existUserName = _userRepository
                                    .GetDbSet()
                                    .Where(us => us.UserName == createUser.username)
                                    .FirstOrDefault();
            if (existUserName != null)
            {
                throw new BadRequestException("User name already exists");
            }

            var existEmail = _userRepository
                                    .GetDbSet()
                                    .Where(us => us.Email == createUser.email)
                                    .FirstOrDefault();
            if (existEmail != null)
            {
                throw new BadRequestException("Email already exists");
            }

            if (createUser.password != createUser.confirmPassword)
            {
                throw new BadRequestException("Password not match");                
            }
            
        }

        public void ValidateUserToUpdate(UpdateUserDto updateUser)
        {
            var existUserName = _userRepository
                                    .GetDbSet()
                                    .Where(us => us.UserName == updateUser.userName && us.Id != updateUser.userId)
                                    .FirstOrDefault();
            if (existUserName != null)
            {
                throw new BadRequestException("User name already exists");
            }

            var existEmail = _userRepository
                                    .GetDbSet()
                                    .Where(us => us.Email == updateUser.email && us.Id != updateUser.userId)
                                    .FirstOrDefault();
            if (existEmail != null)
            {
                throw new BadRequestException("Email already exists");
            }
            
        }
        public int Create(CreateUserDto createUser)
        {
            ValidateUser(createUser);

            User user = new User();
            user.UserName = createUser.username;
            user.Email = createUser.email;
            user.EmployeeCode = createUser.employeeCode;

            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(createUser.password);
            byte[] inArray = SHA1.HashData(vectoBytes);

            string passwordEncrypted = Convert.ToBase64String(inArray);
            user.Password = passwordEncrypted;

            user.CreatedBy = "jason.hernandez";
            user.CreatedAt = DateTime.Now.ToUniversalTime();
            user.Active = true;

            _userRepository.Insert(user);
            _userRepository.SaveChanges();

            return user.Id;
        }

        public void Update(UpdateUserDto update)
        {
            ValidateUserToUpdate(update);

            var user = _userRepository
                        .GetDbSet()
                        .Where(x => x.Id == update.userId)
                        .FirstOrDefault();

            if (user == null)
            {
                throw new BadRequestException("El usuario no existe");
            }

            user.UserName = update.userName;
            user.Email = update.email;
            user.EmployeeCode = update.employeeCode;

            _userRepository.SaveChanges();
        }
        public List<UserGridDto> GetGrid()
        {
            var users = (
                    from us in _userRepository.GetDbSet()
                    where us.Active
                    select new UserGridDto()
                    {
                        Id = us.Id,
                        UserName = us.UserName,
                        Email = us.Email,
                        EmployeeCode = us.EmployeeCode,
                        CreatedAt = us.CreatedAt,
                        CreatedBy = us.CreatedBy,
                        UpdatedAt = us.UpdatedAt,
                        UpdatedBy = us.UpdatedBy,
                        Active = us.Active
                    }
                ).ToList();

            return users;
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

        public void Delete(int userId)
        {
            var user = _userRepository
                        .GetDbSet()
                        .Where(us => us.Id == userId)
                        .FirstOrDefault();

            if (user == null) {
                throw new BadRequestException("User doesn't exists");
            }

            user.Active = false;
            user.UpdatedBy = "jason";
            user.UpdatedAt = DateTime.Now.ToUniversalTime();

            _userRepository.SaveChanges();
        }

        public UserToEditDto GetToEdit(int userId)
        {
            var user = (
                        from us in _userRepository.GetDbSet()
                        where us.Active && us.Id == userId
                        select new UserToEditDto(us.Id, us.UserName, us.Email, us.EmployeeCode)
                        ).FirstOrDefault();

            if (user == null)
            {
                throw new BadRequestException("No se encontro el usuario");
            }

            return user;
        }

        public void ChangePassword(ChangePasswordDto dto)
        {
            var user = _userRepository.GetDbSet().Where(us => us.Id == dto.userId).FirstOrDefault();

            if (user == null) {
                throw new BadRequestException("No se encontro un usuario con ese ID");
            }

            if (dto.password != dto.confirmPassword) {
                throw new BadRequestException("Las contraseñas no coinciden");
            }

            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(user.Password);
            byte[] inArray = SHA1.HashData(vectoBytes);

            string passwordEncrypted = Convert.ToBase64String(inArray);
            user.Password = passwordEncrypted;

            user.UpdatedAt = DateTime.UtcNow;
            string userUpdating = _httpContextAccessor.HttpContext.User.Identity.Name;

            user.UpdatedBy = userUpdating;

            _userRepository.SaveChanges();
        }

        public List<UserResumeDto> GetResume()
        {
            var users = (
                    from us in _userRepository.GetDbSet()
                    where us.Active
                    select new UserResumeDto()
                    {
                        Id = us.Id,
                        UserName = us.UserName
                    }
                ).ToList();

            return users;
        }
    }
}
