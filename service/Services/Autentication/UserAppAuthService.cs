using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Commons;
using UserApp.Service.Global;
using UserApp.Service.Services.Autentication.Dtos;
using UserApp.Service.Services.Users;
using UserApp.Service.Services.Users.Dto;
using UserApp.Service.Services.UsersApplications;

namespace UserApp.Service.Services.Autentication
{
    public class UserAppAuthService : IUserAppAuthService
    {
        IOptions<AppSetting> _options;
        private readonly IUserService _userService;
        private readonly IUserApplicationService _userApplicationService;
        private readonly IRepository<User> _userRepository;
        private static readonly PasswordHasher<User> _passwordHasher = new();
        public UserAppAuthService(
            IUserService userService, 
            IUserApplicationService userApplicationService, 
            IRepository<User> userRepository,
            IOptions<AppSetting> options
         )
        {
            _userService = userService;
            _userApplicationService = userApplicationService;
            _userRepository = userRepository;
            _options = options;
        }

        public AuthDto Login(LoginDto loginDto)
        {
            // El hash (PBKDF2 + salt) no es deterministico, no se puede comparar en SQL:
            // se busca por usuario y se verifica el hash guardado contra la clave recibida.
            var us = _userRepository
                        .GetDbSet()
                        .Where(x => x.UserName == loginDto.userName)
                        .FirstOrDefault();

            if (us is null || string.IsNullOrEmpty(us.Password))
            {
                throw new NotFoundException("Correo no registrado o contraseña incorrecta");
            }

            var verification = _passwordHasher.VerifyHashedPassword(us, us.Password, loginDto.password);
            if (verification == PasswordVerificationResult.Failed)
            {
                throw new NotFoundException("Correo no registrado o contraseña incorrecta");
            }

            UserDto user = new UserDto();
            user.UserName = us.UserName;
            user.Email = us.Email;
            user.EmployeeCode = us.EmployeeCode;

            var userDataJson = JsonSerializer.Serialize(user);

            var claims = new List<Claim> {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_options.Value.JwtSecret)
                    ),
                    SecurityAlgorithms.HmacSha256Signature
                  )
                );

            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new AuthDto(user, token);
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
