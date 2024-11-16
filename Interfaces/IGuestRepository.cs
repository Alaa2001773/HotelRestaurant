using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Interfaces
{
   public interface IGuestRepository
    {
        ICollection<Guest> GetGuests();
        Guest GetGuest(int Id);
        Guest GetGuest(string name);
        bool GuestExists(int Id);
        bool CreateGuest(Guest guest);
        bool UpdateGuest(Guest guest);
        bool DeleteGuest(Guest guest);
        bool Save();
    }
}
