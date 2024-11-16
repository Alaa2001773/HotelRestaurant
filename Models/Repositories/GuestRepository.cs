using Restaurants.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly DataContext _context;

        public GuestRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateGuest(Guest guest)
        {
            _context.Add(guest);

            return Save();
        }

        public bool DeleteGuest(Guest guest)
        {
            _context.Remove(guest);
            return Save();
        }

        public Guest GetGuest(int Id)
        {
            return _context.Guests.Where(p => p.Id == Id).FirstOrDefault();
        }

        public Guest GetGuest(string name)
        {
            return _context.Guests.Where(p => p.Name == name).FirstOrDefault();
        }

        public ICollection<Guest> GetGuests()
        {
            return _context.Guests.OrderBy(r => r.Id).ToList();
        }

        public bool GuestExists(int Id)
        {
            return _context.Guests.Any(r => r.Id == Id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;  
        }

        public bool UpdateGuest(Guest guest)
        {
            _context.Update(guest);
            return Save();
        }
    }
}
