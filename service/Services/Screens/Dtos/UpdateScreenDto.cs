using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Domain.Entities;

namespace UserApp.Service.Services.Screens.Dtos
{
    public record UpdateScreenDto(
        int screenId,
        int applicationId,
        string name,
        string route,
        int? screenFatherId,
        int order,
        bool isFather
        ) : CreateScreenDto(
            applicationId,
            name,
            route,
            screenFatherId,
            order,
            isFather
       );
    
}
