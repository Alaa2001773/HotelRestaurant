using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBuffet { get; set; }
        public int MyProperty { get; set; }
        public string Type { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public int Capacity { get; set; }
        public List<BookaTable> BookaTable { get; set; }
        public List<Menu> Menu { get; set; }
        public List<GiftCard> GiftCards { get; set; }

    }





}
