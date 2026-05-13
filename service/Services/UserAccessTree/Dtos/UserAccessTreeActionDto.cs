namespace UserApp.Service.Services.UserAccessTree.Dtos
{
    public class UserAccessTreeActionDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool HasAccess { get; set; }
    }
}
