using UserApp.Service.Commons;

namespace UserApp.Service.Services.ActionsScreens.Dtos
{
    public class ActionScreenGridDto: BaseDto
    {
        public int ActionId { get; set; }
        public string Action { get; set; }
        public int ScreenId { get; set; }
        public string Screen { get; set; }
        public string Application { get; set; }
    }
}
