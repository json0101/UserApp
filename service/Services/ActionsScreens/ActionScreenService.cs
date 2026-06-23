using service.Commons.Exceptions;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Commons.CurrentUser;
using UserApp.Service.Services.ActionsScreens.Dtos;

namespace UserApp.Service.Services.ActionsScreens
{
    public class ActionScreenService : IActionScreenService
    {
        private readonly IRepository<ScreenAction> _repositoryActionScreen;
        private readonly ICurrentUserService _currentUser;
        public ActionScreenService(IRepository<ScreenAction> repositoryActionScreen, ICurrentUserService currentUser) {
            _repositoryActionScreen = repositoryActionScreen;
            _currentUser = currentUser;
        }

        public List<ActionScreenGridDto> GetGrid()
        {
            var actionScreen = (from screenAction in _repositoryActionScreen.GetDbSet()
                                where screenAction.Active == true
                                select new ActionScreenGridDto()
                                {
                                    Id = screenAction.Id,
                                    ActionId = screenAction.ActionId,
                                    Action = screenAction.Action.Description,
                                    ScreenId = screenAction.ScreenId,
                                    Screen = screenAction.Screen.Name,
                                    Application = screenAction.Screen.Application.Description,
                                    CreatedAt = screenAction.CreatedAt,
                                    CreatedBy = screenAction.CreatedBy,
                                    UpdatedAt = screenAction.UpdatedAt,
                                    UpdatedBy = screenAction.UpdatedBy,
                                    Active = screenAction.Active
                                }
                              ).ToList();

            return actionScreen;
        }

        public int Create(CreateActionScreenDto createActionScreen)
        {
            if(createActionScreen.actionId == 0)
            {
                throw new BadRequestException("Select the action");
            }

            if(createActionScreen.screenId == 0)
            {
                throw new BadRequestException("Select the screen");
            }

            var actionScreen = new ScreenAction();
            actionScreen.ActionId = createActionScreen.actionId;
            actionScreen.ScreenId = createActionScreen.screenId;

            actionScreen.CreatedAt = DateTime.Now.ToUniversalTime();
            actionScreen.CreatedBy = _currentUser.UserName;
            actionScreen.Active = true;

            _repositoryActionScreen.Insert(actionScreen);
            _repositoryActionScreen.SaveChanges();

            return actionScreen.Id;
        }

        public void Delete(int actionScreenId)
        {
            var actionScreen = _repositoryActionScreen.GetDbSet().Where(x => x.Id == actionScreenId).FirstOrDefault();

            if(actionScreen == null)
            {
                throw new BadRequestException("Action Screen doesn't exists");
            }

            actionScreen.Active = false;
            actionScreen.UpdatedAt = DateTime.Now.ToUniversalTime();
            actionScreen.UpdatedBy = _currentUser.UserName;

            _repositoryActionScreen.SaveChanges();
        }

        public ActionScreenEditDto Get(int actionScreenId)
        {
            var actionScreen = (
                    from screenAction in _repositoryActionScreen.GetDbSet()
                    where screenAction.Id == actionScreenId
                    select new ActionScreenEditDto(screenAction.Id, screenAction.ActionId, screenAction.ScreenId)
            ).FirstOrDefault();

            if (actionScreen == null)
            {
                throw new BadRequestException("Action Screen doesn't exists");
            }

            return actionScreen;
        }

        public void Update(UpdateActionScreenDto updateActionScreen)
        {
            var update = _repositoryActionScreen
                            .GetDbSet()
                            .Where(x => x.Id == updateActionScreen.id && x.Active)
                            .FirstOrDefault();


            if (update == null)
            {
                throw new BadRequestException("Action Screen doesn't exists");
            }

            update.ScreenId = updateActionScreen.screenId;
            update.ActionId = updateActionScreen.actionId;

            update.UpdatedBy = _currentUser.UserName;
            update.UpdatedAt = DateTime.Now.ToUniversalTime();

            _repositoryActionScreen.SaveChanges();
        }
    }
}
