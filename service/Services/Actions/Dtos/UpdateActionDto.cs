namespace UserApp.Service.Services.Actions.Dtos
{
    public record UpdateActionDto(int actionId, string description): CreateActionDto(description);
}
