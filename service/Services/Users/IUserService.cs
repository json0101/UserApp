using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Users.Dto;

namespace UserApp.Service.Services.Users
{
    public interface IUserService
    {
        UserDto? GetUserByEmail(string email);
        UserDto? GetUserByEmployeeCod(string employeeCod);
        List<UserGridDto> GetGrid();
        void Update(UpdateUserDto update);
        UserToEditDto GetToEdit(int userId);
        int Create(CreateUserDto createUser);
        void Delete(int userId);
    }
}
