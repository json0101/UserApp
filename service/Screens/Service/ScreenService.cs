using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Screens.Dtos;

namespace UserApp.Service.Screens.Service
{
    public class ScreenService : IScreenService
    {
        public IRepository<Screen> _screenRepository { get; set; }
        public ScreenService(IRepository<Screen> screenRepository) {
            _screenRepository = screenRepository;
        }
        public List<ScreenResumeDto> GetScreens(int application_id)
        {
            var screens = (from i in _screenRepository.GetDbSet()
                           where i.Active && i.ApplicationId == application_id
                           select new ScreenResumeDto(
                                i.Id, 
                                i.Name, 
                                i.Route, 
                                i.ScreenFatherId, 
                                i.ScreenFather == null ? null : i.ScreenFather.Name,
                                i.Order, 
                                i.ApplicationId, 
                                i.Application.Description, 
                                i.IsFather
                             )
                           );

            return screens.ToList();
        }
    }
}
