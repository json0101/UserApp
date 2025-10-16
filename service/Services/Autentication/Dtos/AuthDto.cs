using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Service.Services.Autentication.Dtos
{
    public record AuthDto(UserDto userDto, string token);
}
