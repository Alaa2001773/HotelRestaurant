using Restaurants.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models.Repositories
{
    public class BookaTableRepository : IBookaTableRepository
    {
        private readonly DataContext _context;

        public BookaTableRepository(DataContext context)
        {
            _context = context;
        }
        public bool BookaTableExits(int Id)
        {
            return _context.BookaTables.Any(b => b.Id == Id);
        }

        public bool CreateBookaTable(BookaTable bookaTable, int GuestId ,int restaurantId)
        {
            _context.Add(bookaTable);

            return Save();
        }

        public bool DeleteBookaTable(BookaTable bookaTable)
        {
            _context.Remove(bookaTable);
            return Save();
        }

        public BookaTable GetBookaTable(int Id)
        {
            return _context.BookaTables.Where(b => b.Id == Id ).SingleOrDefault();
           
        }

        public ICollection<BookaTable> GetBookaTables()
        {
            return _context.BookaTables.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBookaTable(BookaTable bookaTable)
        {
            _context.Update(bookaTable);
            return Save();
        }

        public bool UpdateRestaurant(BookaTable bookaTable)
        {
            _context.Update(bookaTable);
            return Save();
        }
    }
}
