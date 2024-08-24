using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly VideoGameContext _context; // Create a private VideoGameContext object called _context

        public CartController(VideoGameContext context) // Constructor that takes a VideoGameContext object as a parameter
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Game? gameToAdd = _context.Games.Where(g => g.GameId == id).SingleOrDefault(); // Get the game from the database with the specified id 

            if (gameToAdd == null) // If the game does not exist
            {
                // Game with specified id does not exist
                TempData["Message"] = "Sorry that game no longer exists"; // Set a message in TempData
                return RedirectToAction("Index", "Games"); // Redirect to the Index action method in the Games controller
            }

            // Todo: Add item to shopping cart cookie
            TempData["Message"] = $"Added {gameToAdd.Title} to cart";
            return RedirectToAction("Index", "Games");
        }
    }
}
