using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.Services.Screens.Dtos
{
    public record CreateScreenDto
    (
        int applicationId,
        string name,
        string route,
        int? screenFatherId,
        int order,
        bool isFather
    );
}
