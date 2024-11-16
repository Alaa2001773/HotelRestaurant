using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models
{
    public class GiftCard
    {
      
        public int Id { get; set; }
        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
        public DateTime ExpiryDate { get; set; }

        public bool IsRedeemed { get; set; }
    }
}
