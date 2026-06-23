using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;
using UserApp.Service.Commons.CurrentUser;
using UserApp.Service.Services.UsersApplications.Dtos;
using service.Commons.Exceptions;

namespace UserApp.Service.Services.UsersApplications
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IRepository<UserApplication> _repositoryUserApplication;
        private readonly ICurrentUserService _currentUser;
        public UserApplicationService(IRepository<UserApplication> repositoryUserApplication, ICurrentUserService currentUser)
        {
            _repositoryUserApplication = repositoryUserApplication;
            _currentUser = currentUser;
        }

        public bool ValidUserApplication(int application_id, int user_id)
        {
            var userApplication = _repositoryUserApplication.GetDbSet().Where(us => us.UserId == user_id && us.ApplicationId == application_id).FirstOrDefault();

            if (userApplication != null)
            {
                return true;
            }

            return false;
        }

        public bool ExistUserApplication(CreateUserApplicationDto create, int? userApplicationId = null)
        {
            var userApplication = _repositoryUserApplication
                            .GetDbSet()
                            .Where(x =>
                                    x.UserId == create.userId
                                    && x.ApplicationId == create.applicationId
                                    && x.Active == true
                                    && (!userApplicationId.HasValue || x.Id != userApplicationId.Value)
                            )
                            .FirstOrDefault();

            if (userApplication != null)
            {
                throw new BadRequestException("Este usuario ya tiene asignada esa aplicacion");
            }

            return true;
        }

        public int CreateUserApplication(CreateUserApplicationDto create)
        {
            ExistUserApplication(create);

            UserApplication userApplication = new UserApplication();
            userApplication.UserId = create.userId;
            userApplication.ApplicationId = create.applicationId;
            userApplication.CreatedAt = DateTime.Now.ToUniversalTime();
            userApplication.CreatedBy = _currentUser.UserName;
            userApplication.Active = true;

            _repositoryUserApplication.Insert(userApplication);
            _repositoryUserApplication.SaveChanges();

            return userApplication.Id;
        }

        public List<UserApplicationResumeDto> GetAllResume()
        {
            var userApplicationResume = (from userApplication in _repositoryUserApplication.GetDbSet()
                                         where userApplication.Active
                                         select new UserApplicationResumeDto(
                                             userApplication.Id,
                                             userApplication.User.UserName,
                                             userApplication.Application.Description
                                         )
                            ).ToList();

            return userApplicationResume;
        }

        public List<UserApplicationGridDto> GetGrid()
        {
            var userApplicationGrid = (from userApplication in _repositoryUserApplication.GetDbSet()
                                       where userApplication.Active
                                       select new UserApplicationGridDto()
                                       {
                                           Id = userApplication.Id,
                                           UserId = userApplication.UserId,
                                           User = userApplication.User.UserName,
                                           Application = userApplication.Application.Description,
                                           ApplicationId = userApplication.ApplicationId,
                                           CreatedAt = userApplication.CreatedAt,
                                           CreatedBy = userApplication.CreatedBy,
                                           UpdatedAt = userApplication.UpdatedAt,
                                           UpdatedBy = userApplication.UpdatedBy,
                                           Active = userApplication.Active,
                                       }
                             ).ToList();

            return userApplicationGrid;
        }

        public void Delete(int userApplicationId)
        {
            var userApplication = _repositoryUserApplication.GetDbSet().Where(userApplication => userApplication.Id == userApplicationId).FirstOrDefault();

            if (userApplication == null)
            {
                throw new Exception("El userApplication no se encontro");
            }

            userApplication.UpdatedAt = DateTime.Now.ToUniversalTime();
            userApplication.UpdatedBy = _currentUser.UserName;
            userApplication.Active = false;
            _repositoryUserApplication.SaveChanges();
        }

        public UserApplicationToEditDto GetUserApplicationToEdit(int userApplicationId)
        {
            var userApplication = (from userApp in _repositoryUserApplication.GetDbSet()
                                   where userApp.Id == userApplicationId
                                   select new UserApplicationToEditDto(userApp.Id, userApp.UserId, userApp.ApplicationId)
                        )
                        .FirstOrDefault();

            if (userApplication == null)
            {
                throw new Exception("No se encontro el userApplication");
            }

            return userApplication;
        }

        public void Update(UpdateUserApplicationDto update)
        {
            ExistUserApplication(update, update.userApplicationId);
            var userApplication = _repositoryUserApplication.GetDbSet().Where(userApplication => userApplication.Id == update.userApplicationId).FirstOrDefault();

            if (userApplication == null)
            {
                throw new Exception("No se encontro el userApplication");
            }

            userApplication.UserId = update.userId;
            userApplication.ApplicationId = update.applicationId;

            userApplication.UpdatedAt = DateTime.Now.ToUniversalTime();
            userApplication.UpdatedBy = _currentUser.UserName;

            _repositoryUserApplication.SaveChanges();
        }
    }
}
