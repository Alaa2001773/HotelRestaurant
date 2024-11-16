using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models
{
    public class Guest

    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@gmail\.com$", ErrorMessage = "InCorrect")]
        public string Email { get; set; }
        public int RoomNumber { get; set; } 
        public string StayDuration { get; set; } 
        public ICollection<BookaTable> BookaTables { get; set; } 
        public ICollection<GiftCard> GiftCards { get; set; } 
    }
}
