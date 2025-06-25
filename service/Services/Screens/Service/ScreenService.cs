using service.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Screens.Dtos;

namespace UserApp.Service.Services.Screens.Service
{
    public class ScreenService : IScreenService
    {
        public IRepository<Screen> _screenRepository { get; set; }
        public ScreenService(IRepository<Screen> screenRepository)
        {
            _screenRepository = screenRepository;
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
            screen.CreatedAt = DateTime.Now;
            screen.CreatedBy = "jason.hernandez";

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

            screen.UpdatedAt = DateTime.Now;
            screen.UpdatedBy = "jason.hernandez";

            _screenRepository.SaveChanges();
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
            screen.UpdatedBy = "jason.hernandez";

            _screenRepository.SaveChanges();
        }
    }
}
