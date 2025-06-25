using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Application.Dtos;

namespace UserApp.Service.Services.Application
{
    public class ApplicationService : IApplicationService
    {
        IRepository<ApplicationRegister> _applicationRepository;
        public ApplicationService(IRepository<ApplicationRegister> applicationRepository) {
            _applicationRepository = applicationRepository;
        }

        public List<ApplicationResumeDto> GetAll()
        {
            var applications = (
                                from v in _applicationRepository.GetDbSet()
                                select new ApplicationResumeDto(v.Id, v.Description)
                              ).ToList();

            return applications;
        }
    }
}
