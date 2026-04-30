namespace UserApp.Service.Services.ActionsScreens.Dtos
{
    public record UpdateActionScreenDto(int id, int actionId, int screenId) : CreateActionScreenDto(actionId, screenId);
}
