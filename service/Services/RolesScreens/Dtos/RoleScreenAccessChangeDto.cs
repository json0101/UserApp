namespace UserApp.Service.Services.RolesScreens.Dtos
{
    public class RoleScreenAccessChangeDto
    {
        public int ScreenId { get; set; }
        public int? RoleScreenId { get; set; }
        public bool HasAccess { get; set; }
    }
}
