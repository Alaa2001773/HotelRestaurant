using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Interfaces
{
    public interface IMenuRepository
    {
        ICollection<Menu> GetMenus();
        Menu GetMenu(int Id);
        bool MenuExists(int Id);
        bool CreateMenu(Menu Menu , int RestaurantId);
        bool UpdateMenu(Menu Menu);
        bool DeleteMenue(Menu Menu);
        bool Save();
    }
}
