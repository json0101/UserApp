namespace UserApp.Service.Services.Screens.Dtos
{
    public record ReorderScreenDto(
        int draggedScreenId,
        int targetScreenId,
        string position
    );
}
