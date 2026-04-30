using UserApp.Service.Services.Actions.Dtos;

namespace UserApp.Service.Services.Actions
{
    public interface IActionService
    {
        List<ActionResumeDto> GetAllResume();
        List<ActionGridDto> GetGrid();
        int CreateAction(CreateActionDto create);
        void Delete(int actionId);
        ActionToEditDto GetActionToEdit(int actionId);
        void Update(UpdateActionDto update);
    }
}
