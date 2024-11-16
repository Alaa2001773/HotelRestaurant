using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Interfaces
{
    public interface IGiftCardRepository
    {
        ICollection<GiftCard> GetGiftCards();
        GiftCard GetGiftCard(int id);
        bool GiftCardExits(int Id);
        bool CreateGiftCard(GiftCard giftCard,int  GuestId,int RestaurantId);
        bool UpdateGiftCard(GiftCard giftCard);
        bool DeleteGiftCard(GiftCard giftCard);
        bool Save();
    }
}
