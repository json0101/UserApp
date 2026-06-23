using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Users.Dto
{
    public record UserToEditDto(
        int id,
        string userName,
        string email,
        string employeeCode,
        string? firstName = null,
        string? lastName = null,
        DateTime? birthDate = null,
        string? country = null,
        string? city = null
    );
}
