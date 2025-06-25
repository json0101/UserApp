using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.UsersApplications
{
    public interface IUserApplicationService
    {
        public bool ValidUserApplication(int application_id, int user_id);
    }
}
