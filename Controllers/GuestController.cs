  using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Interfaces;
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
    public class GuestController : ControllerBase
    {
        private readonly IGuestRepository _guestRepository;
        private readonly DataContext _context;

        public GuestController(IGuestRepository guestRepository,DataContext context)
        {
            _guestRepository = guestRepository;
            _context = context;
        }
        // GET: api/<GuestController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Restaurant>))]
        public IActionResult GetGuests()
        {
            var guest = _guestRepository.GetGuests();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(guest);
        }

        // GET api/<GuestController>/5
        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(Restaurant))]
        [ProducesResponseType(400)] 
        public IActionResult GetGuest(int Id)
        {
            if (!_guestRepository.GuestExists( Id))
                return NotFound();
            var guest = _guestRepository.GetGuest( Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(guest);
        }
        // POST api/<GuestController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateGuest([FromBody] Guest guest)
        {
            if (guest == null)
                return BadRequest(ModelState);

            var newGuest = _guestRepository.GetGuests()
                .Where(c => c.Name.Trim().ToUpper() == guest.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (newGuest != null)
            {
                ModelState.AddModelError("", "Guest already exists");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //
            if (!_guestRepository.CreateGuest(guest))
            {
                ModelState.AddModelError("", "Something went wrong while saving the guest");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        // PUT api/<GuestController>/5
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGuest(int GuestId, Guest guest)
        {
            if (guest == null)
                return BadRequest(ModelState);
            if (GuestId != guest.Id)
                return BadRequest(ModelState);
            if (!_guestRepository.GuestExists(GuestId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            return NoContent();

        }
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{GuestId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteGuest(int GuestId)
        {
            if (!_guestRepository.GuestExists(GuestId))
            {
                return NotFound();
            }
            var GuestToDelete = _guestRepository.GetGuest(GuestId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_guestRepository.DeleteGuest(GuestToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Guest");
            }
            return NoContent();
        }
    }
}

