using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApp.Service.Commons;

namespace UserApp.Service.Services.Applications.Dtos
{
    public class ApplicationGridDto: BaseDto
    {
        public string Description { get; set; }
        
        public ApplicationGridDto() { }
    }
}
