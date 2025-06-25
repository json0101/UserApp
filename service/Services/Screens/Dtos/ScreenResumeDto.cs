using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Screens.Dtos
{
    public record ScreenResumeDto(
        int id,
        string name,
        string route,
        int? screenFatherId,
        string? screenFather,
        string? screenFatherRoute,
        int order,
        int applicationId,
        string application,
        bool isFather
   );
}
