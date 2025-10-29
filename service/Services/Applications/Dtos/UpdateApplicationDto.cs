using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Applications.Dtos
{
    public record UpdateApplicationDto(int appId, string description): CreateApplicationDto(description);
    
}
