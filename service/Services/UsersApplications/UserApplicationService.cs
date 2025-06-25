using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;
using UserApp.Repository;

namespace UserApp.Service.Services.UsersApplications
{
    public class UserApplicationService : IUserApplicationService
    {
        IRepository<UserApplication> _repositoryUserApplication;
        public UserApplicationService(IRepository<UserApplication> repositoryUserApplication)
        {
            _repositoryUserApplication = repositoryUserApplication;
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
    }
}
