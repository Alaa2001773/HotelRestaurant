using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurants.Interfaces;
using Restaurants.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Restaurants.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class BookaTableController : ControllerBase
    {
        private readonly IBookaTableRepository _bookaTableRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly DataContext _context;


        public BookaTableController(IBookaTableRepository bookaTableRepository,
            IGuestRepository guestRepository,
            IRestaurantRepository restaurantRepository

            ,DataContext context)
        {
            _bookaTableRepository = bookaTableRepository;
            _guestRepository = guestRepository;
            _restaurantRepository = restaurantRepository;
            _context = context;
           
        }
        // GET: api/<RestaurantController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookaTable>))]
        public IActionResult GetRestaurant()
        {
            var bookaTable = _bookaTableRepository.GetBookaTables();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bookaTable);
        }

        // GET api/<RestaurantController>/5
        [HttpDelete("GetBookaTable")]
        [ProducesResponseType(200, Type = typeof(BookaTable))]
        [ProducesResponseType(400)]
        public IActionResult GetBookaTable(int Id)
        {
            if (!_bookaTableRepository.BookaTableExits(Id))
                return NotFound();
            var bookaTable = _bookaTableRepository.GetBookaTable(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(bookaTable);
        }

        // POST api/<BookaTableController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult CreateBookaTable([FromBody] BookaTable bookaTable, [FromQuery] int GuestId, [FromQuery] int RestaurantId)
        {
            if (bookaTable == null)
                return BadRequest(ModelState);
            var existingBooking = _context.BookaTables
        .Include(b => b.Restaurant)
        .Include(b => b.Guest)
        .FirstOrDefault(b => b.GuestId == GuestId && b.RestaurantId == RestaurantId);

            if (existingBooking != null)
            {
                ModelState.AddModelError("", "BookATable already exists");

            }
            var restaurant = _restaurantRepository.GetRestaurant(bookaTable.RestaurantId);
            if (restaurant == null)
                return NotFound("Restaurant not found.");

            if (bookaTable.ReservationTime < restaurant.OpeningTime || bookaTable.ReservationTime > restaurant.ClosingTime)
            {
                return BadRequest("Reservation time is outside of working hours.");
            }
            var existingGuests = _context.BookaTables
               .Where(b => b.RestaurantId == bookaTable.RestaurantId && b.ReservationDate == bookaTable.ReservationDate)
               .Sum(b => b.NumberOfGuest);

            if (existingGuests + bookaTable.NumberOfGuest > restaurant.Capacity)
            {
                return BadRequest("Guest count exceeds restaurant capacity.");
            }

            bookaTable.GuestId = GuestId;
            bookaTable.ReservationStatus = "Confirmed";
            if (!_bookaTableRepository.CreateBookaTable(bookaTable, GuestId,RestaurantId))
            {
                ModelState.AddModelError("", "Something went wrong while saving the guest");
                return StatusCode(500, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
       
            return Ok("Successfully created");

        }

        // PUT api/<BookaTableController>/5

        [HttpPut("{bookaTableId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateRestaurant(int bookaTableId, BookaTable bookaTable)
        {
            if (bookaTable == null)
                return BadRequest(ModelState);
            if (bookaTableId != bookaTable.Id)
                return BadRequest(ModelState);
            if (!_bookaTableRepository.BookaTableExits(bookaTableId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            return NoContent();

        }
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{bookaTableId},{RestaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBookaTable(int bookaTableId,int RestaurantId)
        {
            if (!_bookaTableRepository.BookaTableExits(bookaTableId)&& !_restaurantRepository.RestaurantExists(RestaurantId))
            {
                return NotFound();
            }
            var bookaTableToDelete = _bookaTableRepository.GetBookaTable(bookaTableId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_bookaTableRepository.DeleteBookaTable(bookaTableToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting BookaTable");
            }
            return NoContent();
        }
    }
}
