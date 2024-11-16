using DevExpress.Data.Browsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models.Repositories
{
    public class RestaurantRepository:IRestaurantRepository
    {
        private readonly DataContext _context;

        public RestaurantRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateRestaurant(Restaurant restaurant)
        {
            _context.Add(restaurant);

           return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            return Save();

        }

        public Restaurant GetRestaurant(int Id)
        {
            return _context.Restaurants.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Restaurant GetRestaurant(string name)
        {
            return _context.Restaurants.Where(p => p.Name == name).FirstOrDefault();
        }


        public ICollection<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.OrderBy(r => r.Id).ToList();
            
        }

        public bool RestaurantExists(int Id)
        {
            return _context.Restaurants.Any(r => r.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRestaurant(Restaurant restaurant)
        {
            _context.Update(restaurant);
            return Save();
        }
    }
}
