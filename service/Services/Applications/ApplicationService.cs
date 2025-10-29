using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Services.Applications.Dtos;


namespace UserApp.Service.Services.Applications
{
    public class ApplicationService : IApplicationService
    {
        private readonly IRepository<Application> _appRepository;
        public ApplicationService(IRepository<Application> appRepository) {
            _appRepository = appRepository;
        }

        public int CreateApplication(CreateApplicationDto create)
        {
            Application app = new Application();
            app.Description = create.description;
            app.CreatedAt = DateTime.Now.ToUniversalTime();
            app.CreatedBy = "";
            app.Active = true;

            _appRepository.Insert(app);
            _appRepository.SaveChanges();

            return app.Id;
        }

        public List<ApplicationResumeDto> GetAllResume()
        {
            var appResume = (from r in _appRepository.GetDbSet()
                             where r.Active
                             select new ApplicationResumeDto(r.Id, r.Description)
                             ).ToList();

            return appResume;
        }

        public List<ApplicationGridDto> GetGrid()
        {
            var appGrid = (from app in _appRepository.GetDbSet()
                             where app.Active
                             select new ApplicationGridDto()
                                 {
                                     Id = app.Id,
                                     Description = app.Description,
                                     CreatedAt = app.CreatedAt,
                                     CreatedBy = app.CreatedBy,
                                     UpdatedAt = app.UpdatedAt,
                                     UpdatedBy = app.UpdatedBy,
                                     Active = app.Active,
                                 }       
                             ).ToList();

            return appGrid;
        }

        public void Delete(int appId)
        {
            var app = _appRepository.GetDbSet().Where(app => app.Id == appId).FirstOrDefault();

            if (app == null) {
                throw new Exception("El app no se encontro");
            }

            app.UpdatedAt = DateTime.Now.ToUniversalTime();
            app.UpdatedBy = "";
            app.Active = false;
            _appRepository.SaveChanges();
        }

        public ApplicationToEditDto GetApplicationToEdit(int appId)
        {
            var apli = (from app in _appRepository.GetDbSet()
                            where app.Id == appId
                            select new ApplicationToEditDto(app.Id, app.Description)
                        )
                        .FirstOrDefault();

            if (apli == null)
            {
                throw new Exception("No se encontro el app");
            }

            return apli;
        }

        public void Update(UpdateApplicationDto update)
        {
            var app = _appRepository.GetDbSet().Where(app => app.Id == update.appId).FirstOrDefault();

            if (app == null)
            {
                throw new Exception("No se encontro el app");
            }

            app.Description = update.description;

            app.UpdatedAt = DateTime.Now.ToUniversalTime();
            app.UpdatedBy = "";
            
            _appRepository.SaveChanges();
        }
    }
}
