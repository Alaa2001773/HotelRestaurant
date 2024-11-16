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
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly DataContext _context;

        public MenuController(IMenuRepository menuRepository
            ,IRestaurantRepository restaurantRepository
            ,DataContext context)
        {
            _menuRepository = menuRepository;
            _restaurantRepository = restaurantRepository;
            _context = context;
        }
        // GET: api/<MenuController>
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(IEnumerable<Menu>))]
        public IActionResult GetMenu()
        {
            var menu = _menuRepository.GetMenus();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(menu);
        }

        // GET api/<MenuController>/5
        [HttpGet("{Id}")]
        [ProducesResponseType(200,Type=typeof(Menu))]
        [ProducesResponseType(400)]
        public  IActionResult GetMenu(int Id)
        {
            if (!_menuRepository.MenuExists(Id))
                return NotFound();
            var menu = _menuRepository.GetMenu(Id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(menu);

        }

        // POST api/<MenuController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        // POST api/<MenuController>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateMenu([FromBody] Menu menu, [FromQuery] int RestaurantId)
        {
            if (menu == null)
                return BadRequest("Menu data is required.");

            // تحقق من وجود المطعم باستخدام RestaurantId
            var restaurant = _restaurantRepository.GetRestaurant(RestaurantId);  // هنا تتأكد من أن المطعم موجود
            if (restaurant == null)
            {
                ModelState.AddModelError("", "Restaurant not found.");
                return BadRequest(ModelState);  // إذا كان المطعم غير موجود، أرسل خطأ
            }

            // تحقق من وجود القائمة بنفس الاسم
            var existingMenu = _menuRepository.GetMenus()
                .FirstOrDefault(m => m.Name.Trim().ToUpper() == menu.Name.Trim().ToUpper());
            if (existingMenu != null)
            {
                ModelState.AddModelError("", "Menu already exists.");
                return BadRequest(ModelState);
            }

            // إذا تم التحقق من المطعم، قم بربط القائمة بالمطعم
            menu.RestaurantId = RestaurantId;

            // إضافة القائمة إلى السياق
            _context.Add(menu);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // استخدام المستودع لإنشاء القائمة
            if (!_menuRepository.CreateMenu(menu, RestaurantId))
            {
                ModelState.AddModelError("", "Something went wrong while saving the menu.");
                return StatusCode(500, ModelState);
            }

            return Ok("Menu successfully created.");
        }


        // PUT api/<MenuController>/5
        [HttpPut("{MenuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCategory(int MenuId, Menu menu)
        {
            if (menu == null)
                return BadRequest(ModelState);
            if (MenuId != menu.Id)
                return BadRequest(ModelState);
            if (!_menuRepository.MenuExists(MenuId))
                return NotFound();
            if (!ModelState.IsValid)
                return BadRequest();
            return NoContent();

        }
        // DELETE api/<RestaurantController>/5
        [HttpDelete("{MenuId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteMenue(int MenuId)
        {
            if (!_menuRepository.MenuExists(MenuId))
            {
                return NotFound();
            }
            var menuToDelete = _menuRepository.GetMenu(MenuId);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!_menuRepository.DeleteMenue(menuToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting Menu");
            }
            return NoContent();
        }
    }
}
