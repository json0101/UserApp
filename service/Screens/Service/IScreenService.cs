using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Screens.Dtos;

namespace UserApp.Service.Screens.Service
{
    public interface IScreenService
    {
        List<ScreenResumeDto> GetScreens(int application_id);
    }
}
