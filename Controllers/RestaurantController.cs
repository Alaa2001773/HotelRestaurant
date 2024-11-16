using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Restaurants.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly DataContext _context;

        public RestaurantController(IRestaurantRepository restaurantRepository, DataContext context)
        {
            _restaurantRepository = restaurantRepository;
            _context = context;
        }
        // GET: api/<RestaurantController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        public IActionResult GetRestaurant()
        {
            var restaurant = _restaurantRepository.GetRestaurants();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(restaurant);
        }

        // GET api/<RestaurantController>/5
        [HttpDelete("{Id}")]
        [ProducesResponseType(200, Type = typeof(Restaurant))]
        [ProducesResponseType(400)]
        public IActionResult GetRestaurant(int Id)
        {
            if (!_restaurantRepository.RestaurantExists(Id))
                return NotFound();
            var restaurant = _restaurantRepository.GetRestaurant(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(restaurant);
        }

        // POST api/<RestaurantController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateRestaurant([FromBody] Restaurant restaurant)
        {
            if (restaurant == null)
                return BadRequest(ModelState);
            var newRastaurant = _restaurantRepository.GetRestaurants()
                .Where(c => c.Name.Trim().ToUpper() == restaurant.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
            if (restaurant != null)
            {
                ModelState.AddModelError("", "Restaurant already exists");

            }
            if (!_restaurantRepository.CreateRestaurant(restaurant))
            {
                ModelState.AddModelError("", "Something went wrong while saving the guest");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok("Successfully created");


        }


        // PUT api/<RestaurantController>/5
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
         public IActionResult UpdateRestaurant(int restaurantId,Restaurant restaurant )
        {
            if (restaurant == null)
                return BadRequest(ModelState);
            if (restaurantId != restaurant.Id)
                return BadRequest(ModelState);
            if (!_restaurantRepository.RestaurantExists(restaurantId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            return NoContent();
            
        }
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteRestaurant(int restaurantId)
        {
            if (!_restaurantRepository.RestaurantExists(restaurantId))
            {
                return NotFound();
            }
            var restrauntToDelete = _restaurantRepository.GetRestaurant(restaurantId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_restaurantRepository.DeleteRestaurant(restrauntToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Restaurant");
            }
            return NoContent();
        }
    }
}
