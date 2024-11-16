using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurants.Models
{
    public class Menu
    {
       
        public int Id { get; set; }
        public string Name { get; set; }  
        public decimal Price { get; set; }  
        public string Ingredients { get; set; }
        public int RestaurantId { get; set; } 
        public Restaurant Restaurant { get; set; }
    }
}
