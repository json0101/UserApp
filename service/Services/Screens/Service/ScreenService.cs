using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Commons.CurrentUser;
using UserApp.Service.Services.Screens.Dtos;

namespace UserApp.Service.Services.Screens.Service
{
    public class ScreenService : IScreenService
    {
        public IRepository<Screen> _screenRepository { get; set; }
        private readonly ICurrentUserService _currentUser;
        public ScreenService(IRepository<Screen> screenRepository, ICurrentUserService currentUser)
        {
            _screenRepository = screenRepository;
            _currentUser = currentUser;
        }
        public List<ScreenResumeDto> GetScreens(int application_id)
        {
            var screens = from i in _screenRepository.GetDbSet()
                           where i.Active && i.ApplicationId == application_id
                           select new ScreenResumeDto(
                                i.Id,
                                i.Name,
                                i.Route,
                                i.ScreenFatherId,
                                i.ScreenFather == null ? null : i.ScreenFather.Name,
                                i.ScreenFather == null ? null : i.ScreenFather.Route,
                                i.Order,
                                i.ApplicationId ?? 0,
                                i.Application.Description,
                                i.IsFather
                             )
                           ;

            return screens.ToList();
        }

        public List<ScreenResumeDto> GetScreens()
        {
            var screens = from i in _screenRepository.GetDbSet()
                          where i.Active
                          select new ScreenResumeDto(
                               i.Id,
                               i.Name,
                               i.Route,
                               i.ScreenFatherId,
                               i.ScreenFather == null ? null : i.ScreenFather.Name,
                               i.ScreenFather == null ? null : i.ScreenFather.Route,
                               i.Order,
                               i.ApplicationId ?? 0,
                               i.Application.Description,
                               i.IsFather
                            )
                           ;

            return screens.ToList();
        }

        public List<ScreenResumeDto> GetScreensFathers()
        {
            var screens = from i in _screenRepository.GetDbSet()
                          where i.Active && i.IsFather
                          select new ScreenResumeDto(
                               i.Id,
                               i.Name,
                               i.Route,
                               i.ScreenFatherId,
                               i.ScreenFather == null ? null : i.ScreenFather.Name,
                               i.ScreenFather == null ? null : i.ScreenFather.Route,
                               i.Order,
                               i.ApplicationId ?? 0,
                               i.Application.Description,
                               i.IsFather
                            )
                           ;

            return screens.ToList();
        }

        public ScreenResumeDto GetScreen(int id)
        {
            var screen = (from i in _screenRepository.GetDbSet()
                          where i.Active && i.Id == id
                          select new ScreenResumeDto(
                                i.Id,
                                i.Name,
                                i.Route,
                                i.ScreenFatherId,
                                i.ScreenFather == null ? null : i.ScreenFather.Name,
                                i.ScreenFather == null ? null : i.ScreenFather.Route,
                                i.Order,
                                i.ApplicationId ?? 0,
                                i.Application.Description,
                                i.IsFather
                          )).FirstOrDefault();

            if (screen == null)
            {
                throw new NotFoundException("Screen Not Found");
            }

            return screen;
        }

        public int Save(CreateScreenDto createScreenDto)
        {
            Screen screen = new Screen();
            screen.ApplicationId = createScreenDto.applicationId;
            screen.Name = createScreenDto.name;
            screen.Route = createScreenDto.route;
            screen.ScreenFatherId = createScreenDto.screenFatherId;
            screen.Order = createScreenDto.order;
            screen.IsFather = createScreenDto.isFather;

            screen.Active = true;
            screen.CreatedAt = DateTime.Now.ToUniversalTime();
            screen.CreatedBy = _currentUser.UserName;

            _screenRepository.Insert(screen);
            _screenRepository.SaveChanges();

            return screen.Id;
        }

        public void Update(UpdateScreenDto updateScreenDto)
        {
            var screen = _screenRepository.GetDbSet().Where(s => s.Id == updateScreenDto.screenId).FirstOrDefault();

            if (screen == null)
            {
                throw new NotFoundException("Screen Not Found");
            }

            screen.ApplicationId = updateScreenDto.applicationId;
            screen.Name = updateScreenDto.name;
            screen.Route = updateScreenDto.route;
            screen.ScreenFatherId = updateScreenDto.screenFatherId;
            screen.Order = updateScreenDto.order;
            screen.IsFather = updateScreenDto.isFather;

            screen.UpdatedAt = DateTime.Now.ToUniversalTime();
            screen.UpdatedBy = _currentUser.UserName;

            _screenRepository.SaveChanges();
        }

        public void Reorder(ReorderScreenDto reorderScreenDto)
        {
            var position = reorderScreenDto.position?.Trim().ToLowerInvariant();

            if (position != "before" && position != "after" && position != "child")
            {
                throw new BadRequestException("Invalid drop position");
            }

            if (reorderScreenDto.draggedScreenId == reorderScreenDto.targetScreenId)
            {
                return;
            }

            var screens = _screenRepository.GetDbSet()
                .Where(screen => screen.Active)
                .ToList();

            var draggedScreen = screens.FirstOrDefault(screen => screen.Id == reorderScreenDto.draggedScreenId);
            var targetScreen = screens.FirstOrDefault(screen => screen.Id == reorderScreenDto.targetScreenId);

            if (draggedScreen == null || targetScreen == null)
            {
                throw new NotFoundException("Screen Not Found");
            }

            if (draggedScreen.ApplicationId != targetScreen.ApplicationId)
            {
                throw new BadRequestException("Screens must belong to the same application");
            }

            if (position == "child")
            {
                ReorderAsChild(screens, draggedScreen, targetScreen);
            }
            else
            {
                ReorderAsSibling(screens, draggedScreen, targetScreen, position);
            }

            _screenRepository.SaveChanges();
        }

        private void ReorderAsChild(List<Screen> screens, Screen draggedScreen, Screen targetScreen)
        {
            if (IsDescendantScreen(screens, draggedScreen.Id, targetScreen.Id))
            {
                throw new BadRequestException("No se puede mover una pantalla dentro de uno de sus hijos.");
            }

            var nextFatherId = targetScreen.Id;
            var nextOrder = GetSiblingScreens(screens, draggedScreen.ApplicationId, nextFatherId, draggedScreen.Id)
                .Select(screen => screen.Order)
                .DefaultIfEmpty(0)
                .Max() + 1;

            draggedScreen.ScreenFatherId = nextFatherId;
            draggedScreen.Order = nextOrder;
            TouchScreen(draggedScreen);
        }

        private void ReorderAsSibling(List<Screen> screens, Screen draggedScreen, Screen targetScreen, string position)
        {
            if (IsDescendantScreen(screens, draggedScreen.Id, targetScreen.Id))
            {
                throw new BadRequestException("No se puede mover una pantalla junto a uno de sus hijos.");
            }

            var previousFatherId = draggedScreen.ScreenFatherId;
            var nextFatherId = targetScreen.ScreenFatherId;

            if (previousFatherId == nextFatherId && IsSameVisualPosition(screens, draggedScreen, targetScreen, position))
            {
                return;
            }

            var insertOrder = position == "after"
                ? targetScreen.Order + 1
                : targetScreen.Order;

            var siblingsToShift = GetSiblingScreens(screens, draggedScreen.ApplicationId, nextFatherId, draggedScreen.Id)
                .Where(screen => screen.Order >= insertOrder)
                .ToList();

            foreach (var screen in siblingsToShift)
            {
                screen.Order += 1;
                TouchScreen(screen);
            }

            draggedScreen.ScreenFatherId = nextFatherId;
            draggedScreen.Order = insertOrder;
            TouchScreen(draggedScreen);
        }

        private static List<Screen> GetSiblingScreens(List<Screen> screens, int? applicationId, int? screenFatherId, int? exceptScreenId = null)
        {
            return screens
                .Where(screen =>
                    screen.ApplicationId == applicationId
                    && screen.ScreenFatherId == screenFatherId
                    && (!exceptScreenId.HasValue || screen.Id != exceptScreenId.Value))
                .OrderBy(screen => screen.Order)
                .ThenBy(screen => screen.Name)
                .ToList();
        }

        private static bool IsSameVisualPosition(List<Screen> screens, Screen draggedScreen, Screen targetScreen, string position)
        {
            var siblings = GetSiblingScreens(screens, draggedScreen.ApplicationId, draggedScreen.ScreenFatherId);
            var draggedIndex = siblings.FindIndex(screen => screen.Id == draggedScreen.Id);
            var targetIndex = siblings.FindIndex(screen => screen.Id == targetScreen.Id);

            return (position == "before" && draggedIndex == targetIndex - 1)
                || (position == "after" && draggedIndex == targetIndex + 1);
        }

        private void TouchScreen(Screen screen)
        {
            screen.UpdatedAt = DateTime.Now.ToUniversalTime();
            screen.UpdatedBy = _currentUser.UserName;
        }

        private static bool IsDescendantScreen(List<Screen> screens, int screenId, int possibleDescendantId)
        {
            var parentId = screens.FirstOrDefault(screen => screen.Id == possibleDescendantId)?.ScreenFatherId;

            while (parentId != null)
            {
                if (parentId == screenId)
                {
                    return true;
                }

                parentId = screens.FirstOrDefault(screen => screen.Id == parentId)?.ScreenFatherId;
            }

            return false;
        }

        public void Delete(int id)
        {
            var screen = _screenRepository.GetDbSet().Where(s => s.Id == id).FirstOrDefault();

            if (screen == null)
            {
                throw new NotFoundException("Screen Not Found");
            }

            screen.Active = false;
            screen.UpdatedAt = DateTime.Now;
            screen.UpdatedBy = _currentUser.UserName;

            _screenRepository.SaveChanges();
        }
    }
}
