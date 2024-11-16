using Microsoft.EntityFrameworkCore;
using Restaurants.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models.Repositories
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private readonly DataContext _context;

        public GiftCardRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<GiftCard> GetGiftCards()
        {
           return _context.GiftCards.Include(g => g.Guest).Include(g => g.Restaurant).ToList();
        }

        public GiftCard GetGiftCard(int id)
        {
           return _context.GiftCards.Include(g => g.Guest).Include(g => g.Restaurant)
                                                          .SingleOrDefault(g => g.Id == id);
        }

        public bool GiftCardExits(int Id)
        {
            return _context.Restaurants.Any(r => r.Id == Id);
        }

        public bool CreateGiftCard(GiftCard giftCard, int GuestId, int RestaurantId)
        {
            _context.Add(giftCard);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateGiftCard(GiftCard giftCard)
        {
            _context.Update(giftCard);
            return Save();
        }

        public bool DeleteGiftCard(GiftCard giftCard)
        {
            _context.Remove(giftCard);
            return Save(); ;
        }
    }
}
