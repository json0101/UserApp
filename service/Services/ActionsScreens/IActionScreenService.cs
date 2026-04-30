using UserApp.Service.Services.ActionsScreens.Dtos;

namespace UserApp.Service.Services.ActionsScreens
{
    public interface IActionScreenService
    {
        List<ActionScreenGridDto> GetGrid();
        ActionScreenEditDto Get(int actionScreenId);
        int Create(CreateActionScreenDto createActionScreen);
        void Update(UpdateActionScreenDto updateActionScreen);
        void Delete(int actionScreenId);
    }
}
