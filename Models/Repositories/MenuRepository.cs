using Microsoft.EntityFrameworkCore;
using Restaurants.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly DataContext _context;

        public MenuRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateMenu(Menu menu, int RestaurantId)
        {
            //var restaurantEntity = _context.Restaurants.Where(r => r.Id == RestaurantId).FirstOrDefault();
            //var arestaurant = new Restaurant()
            //{
            //    Restaurant = restaurantEntity,
            //    Menu = menu,
            //};
            _context.Add(menu);
            return Save();
        }

        public bool DeleteMenue(Menu menu)
        {
            _context.Remove(menu);
            return Save();
        }

        public Menu GetMenu(int Id)
        {
            return _context.Menus.Include(m => m.Restaurant).SingleOrDefault(m => m.Id == Id);

        }

        public ICollection<Menu> GetMenus()
        {
            return _context.Menus.OrderBy(m => m.Id).ToList();
        }

        public bool MenuExists(int Id)
        {
            return _context.Restaurants.Any(m => m.Id == Id);

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateMenu(Menu menu)
        {
            _context.Update(menu);
            return Save();
        }
    }
}
