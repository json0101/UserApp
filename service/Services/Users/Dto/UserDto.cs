﻿using UserApp.Service.Commons;

namespace UserApp.Service.Services.Users.Dto
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int? CountryId { get; set; }
        public int? AddressId { get; set; }
        public string EmployeeCode { get; set; }
    }
}
