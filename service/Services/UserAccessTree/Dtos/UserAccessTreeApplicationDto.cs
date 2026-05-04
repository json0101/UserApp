namespace UserApp.Service.Services.UserAccessTree.Dtos
{
    public class UserAccessTreeApplicationDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<UserAccessTreeScreenDto> Screens { get; set; } = new();
    }
}
