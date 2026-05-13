namespace UserApp.Service.Services.UserAccessTree.Dtos
{
    public class UserAccessTreeScreenDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Route { get; set; } = string.Empty;
        public int Order { get; set; }
        public int? ScreenFatherId { get; set; }
        public bool IsFather { get; set; }
        public bool HasAccess { get; set; }
        public int? RoleScreenId { get; set; }
        public List<UserAccessTreeActionDto> Actions { get; set; } = new();
    }
}
