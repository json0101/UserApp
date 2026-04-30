using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Actions.Dtos;

namespace UserApp.Service.Services.Actions
{
    public class ActionService : IActionService
    {
        private readonly IRepository<ActionSys> _actionRepository;
        public ActionService(IRepository<ActionSys> actionRepository) {
            _actionRepository = actionRepository;
        }

        public int CreateAction(CreateActionDto create)
        {
            ActionSys action = new ActionSys();
            action.Description = create.description;
            action.CreatedAt = DateTime.Now.ToUniversalTime();
            action.CreatedBy = "";
            action.Active = true;

            _actionRepository.Insert(action);
            _actionRepository.SaveChanges();

            return action.Id;
        }

        public List<ActionResumeDto> GetAllResume()
        {
            var actionResume = (from action in _actionRepository.GetDbSet()
                                where action.Active
                                select new ActionResumeDto(action.Id, action.Description)
                             ).ToList();

            return actionResume;
        }

        public List<ActionGridDto> GetGrid()
        {
            var actionGrid = (from action in _actionRepository.GetDbSet()
                              where action.Active
                              select new ActionGridDto()
                              {
                                  Id = action.Id,
                                  Description = action.Description,
                                  CreatedAt = action.CreatedAt,
                                  CreatedBy = action.CreatedBy,
                                  UpdatedAt = action.UpdatedAt,
                                  UpdatedBy = action.UpdatedBy,
                                  Active = action.Active,
                              }
                             ).ToList();

            return actionGrid;
        }

        public void Delete(int actionId)
        {
            var action = _actionRepository.GetDbSet().Where(action => action.Id == actionId).FirstOrDefault();

            if (action == null) {
                throw new Exception("El action no se encontro");
            }

            action.UpdatedAt = DateTime.Now.ToUniversalTime();
            action.UpdatedBy = "";
            action.Active = false;
            _actionRepository.SaveChanges();
        }

        public ActionToEditDto GetActionToEdit(int actionId)
        {
            var action = (from actionSys in _actionRepository.GetDbSet()
                          where actionSys.Id == actionId
                          select new ActionToEditDto(actionSys.Id, actionSys.Description)
                        )
                        .FirstOrDefault();

            if (action == null)
            {
                throw new Exception("No se encontro el action");
            }

            return action;
        }

        public void Update(UpdateActionDto update)
        {
            var action = _actionRepository.GetDbSet().Where(action => action.Id == update.actionId).FirstOrDefault();

            if (action == null)
            {
                throw new Exception("No se encontro el action");
            }

            action.Description = update.description;

            action.UpdatedAt = DateTime.Now.ToUniversalTime();
            action.UpdatedBy = "";
            
            _actionRepository.SaveChanges();
        }
    }
}
