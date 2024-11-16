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
    public class GiftCardController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IGiftCardRepository _giftCardRepository;

        public GiftCardController(DataContext Context,IGiftCardRepository giftCardRepository)
        {
            _context = Context;
            _giftCardRepository = giftCardRepository;
        }
        // GET: api/<GiftCardController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<BookaTable>))]
        public IActionResult GetRestaurant()
        {
            var giftCard = _giftCardRepository.GetGiftCards();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(giftCard);
        }
        // GET api/<GiftCardController>/5
        [HttpDelete("GetGiftCard")]
        [ProducesResponseType(200, Type = typeof(GiftCard))]
        [ProducesResponseType(400)]
        public IActionResult GetGiftCard(int id)
        {
            if (!_giftCardRepository.GiftCardExits(id))
                return NotFound();
            var giftCard = _giftCardRepository.GetGiftCard( id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(giftCard);
        }

        // POST api/<GiftCardController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateGiftCard([FromBody] GiftCard giftCard, [FromQuery] int GuestId, [FromQuery] int RestaurantId)
        {
            if (giftCard == null)
                return BadRequest(ModelState);
            var newRastaurant = _giftCardRepository.GetGiftCards()
                .Where(c => c.Id== giftCard.Id)
                .FirstOrDefault();
            if (giftCard.ExpiryDate < DateTime.Now)
            {
                return BadRequest("Expiry date cannot be in the past.");
            }
            if (giftCard != null)
            {
                ModelState.AddModelError("", "giftCard already exists");

            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_giftCardRepository.CreateGiftCard(giftCard, GuestId,RestaurantId))
            {
                ModelState.AddModelError("", "Something went wrong while saving the guest");
                return StatusCode(500, ModelState);
            }
            return Ok("Successfully created"); }

        // PUT api/<RestaurantController>/5
        [HttpPut("{restaurantId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateGiftCard(int GiftCardId, GiftCard giftCard)
        {
            if (giftCard == null)
                return BadRequest(ModelState);
            if (GiftCardId != giftCard.Id)
                return BadRequest(ModelState);
            if (!_giftCardRepository.GiftCardExits(GiftCardId))
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

        public IActionResult DeleteRestaurant(int GiftId)
        {
            if (!_giftCardRepository.GiftCardExits(GiftId))
            {
                return NotFound();
            }
            var GiftCardToDelete = _giftCardRepository.GetGiftCard(GiftId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_giftCardRepository.DeleteGiftCard(GiftCardToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting GiftCard");
            }
            return NoContent();
        }
    }
}
