using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Services.Application.Dtos;

namespace UserApp.Service.Services.Application
{
    public interface IApplicationService
    {
        public List<ApplicationResumeDto> GetAll();
    }
}
