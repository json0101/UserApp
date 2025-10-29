using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Applications.Dtos;

namespace UserApp.Service.Services.Applications
{
    public interface IApplicationService
    {
        List<ApplicationResumeDto> GetAllResume();
        List<ApplicationGridDto> GetGrid();
        int CreateApplication(CreateApplicationDto create);
        void Delete(int appId);
        ApplicationToEditDto GetApplicationToEdit(int appId);
        void Update(UpdateApplicationDto update);
    }
}
