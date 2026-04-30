using UserApp.Service.Commons;

namespace UserApp.Service.Services.UsersApplications.Dtos
{
    public class UserApplicationGridDto: BaseDto
    {
        public int UserId { get; set; }
        public string User { get; set; }
        public int ApplicationId { get; set; }
        public string Application { get; set; }

        public UserApplicationGridDto() { }
    }
}
