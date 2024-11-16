using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Interfaces
{
   public interface IBookaTableRepository
    {
        ICollection<BookaTable> GetBookaTables();
        BookaTable GetBookaTable(int Id);
        bool BookaTableExits(int Id);
        bool CreateBookaTable(BookaTable bookaTable, int GuestId,int restaurantId);
        bool UpdateBookaTable(BookaTable bookaTable);
        bool DeleteBookaTable(BookaTable bookaTable);
        bool Save();
    }
}
