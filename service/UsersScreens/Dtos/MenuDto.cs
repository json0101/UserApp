using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.Service.UsersScreens.Dtos
{
    public class MenuDto
    {
        public int ScreenId { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public bool IsFather { get; set; }
        public int? ScreenFatherId { get; set; }
        public int Order { get; set; }
        public List<MenuDto> Children { get; set; }
    }
}
