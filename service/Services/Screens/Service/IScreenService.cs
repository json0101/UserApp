using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Screens.Dtos;

namespace UserApp.Service.Services.Screens.Service
{
    public interface IScreenService
    {
        List<ScreenResumeDto> GetScreens(int application_id);
        List<ScreenResumeDto> GetScreens();
        List<ScreenResumeDto> GetScreensFathers();
        ScreenResumeDto GetScreen(int id);
        int Save(CreateScreenDto createScreenDto);
        void Update(UpdateScreenDto updateScreenDto);
        void Delete(int id);
    }
}
