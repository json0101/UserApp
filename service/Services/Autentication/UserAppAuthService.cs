using Microsoft.EntityFrameworkCore;
using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Global;
using UserApp.Service.Services.Autentication.Dtos;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Dto;
using UserApp.Service.Services.UsersApplications;

namespace UserApp.Service.Services.Autentication
{
    public class UserAppAuthService : IUserAppAuthService
    {
        private readonly IUserService _userService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IRepository<User> _userRepository;
        public UserAppAuthService(
            IUserService userService, 
            IUserApplicationService userApplicationService, 
            IRepository<User> userRepository)
        {
            _userService = userService;
            _userApplicationService = userApplicationService;
            _userRepository = userRepository;
        }

        

        public UserDto Login(LoginDto loginDto)
        {
            byte[] vectoBytes = System.Text.Encoding.UTF8.GetBytes(loginDto.password);
            byte[] inArray = SHA1.HashData(vectoBytes);

            string passwordEncrypted = Convert.ToBase64String(inArray);

            var us = _userRepository
                        .GetDbSet()
                        .Where(
                            x => x.UserName == loginDto.userName 
                            && x.Password == passwordEncrypted
                        )
                        .FirstOrDefault();

            if (us is null)
            {
                throw new NotFoundException("Correo no registrado o contraseña incorrecta");
            }

            UserDto user = new UserDto();
            user.UserName = us.UserName;
            user.Email = us.Email;
            user.EmployeeCode = us.EmployeeCode;

            return user;
        }

        public UserDto? UserValidByEmployeeCod(string employeeCod, out string message)
        {
            var user = _userService.GetUserByEmployeeCod(employeeCod);

            if (user == null)
            {
                message = "User not found";
                return null;
            }

            var validUserApplication = _userApplicationService.ValidUserApplication(ApplicationGlobal.ApplicationGlobalID, user.Id);

            if (validUserApplication == false)
            {
                message = "User doesn't have permissions in this applications";
                return null;
            }

            message = "Correct";
            return user;
        }
    }
}
