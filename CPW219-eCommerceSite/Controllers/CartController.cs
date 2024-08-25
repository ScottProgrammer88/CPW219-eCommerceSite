using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CPW219_eCommerceSite.Controllers
{
    public class CartController : Controller
    {
        private readonly VideoGameContext _context; // Create a private VideoGameContext object called _context
        private const string Cart = "ShoppingCart"; // Create a constant string called Cart and set it to "ShoppingCart"

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

            CartGameViewModel cartGame = new() // Create a new CartGameViewModel object called cartGame
            {
                GameId = gameToAdd.GameId, // Set the GameId property of cartGame to the GameId of gameToAdd
                Title = gameToAdd.Title, // Set the Title property of cartGame to the Title of gameToAdd
                Price = gameToAdd.Price // Set the Price property of cartGame to the Price of gameToAdd
            };

            List<CartGameViewModel> cartGames = GetExistingCartData(); // Create a new List of CartGameViewModel objects called cartGames
            cartGames.Add(cartGame); // Add the cartGame object to the cartGames list

            WriteShoppingCartCookie(cartGames);

            // Todo: Add item to shopping cart cookie
            TempData["Message"] = $"Added {gameToAdd.Title} to cart";
            return RedirectToAction("Index", "Games");
        }

        private void WriteShoppingCartCookie(List<CartGameViewModel> cartGames)
        {
            string cookieData = JsonConvert.SerializeObject(cartGames); // Serialize the cartGames list to a JSON string

            HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions() // Add the cartGame object to the Cart cookie
            {
                Expires = DateTimeOffset.Now.AddYears(1) // Set the expiration date of the cookie to 1 year from now
            });
        }

        /// <summary>
        /// Return the current list of video games in the users shopping cart cookie. 
        /// If there is no cookie, an empty list will be returned
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private List<CartGameViewModel> GetExistingCartData()
        {
            string? cookie = HttpContext.Request.Cookies[Cart]; // Get the value of the ShoppingCart cookie
            if (string.IsNullOrEmpty(cookie)) // If the cookie is null or empty
            {
                return new List<CartGameViewModel>(); // Return an empty list
            }

            return JsonConvert.DeserializeObject<List<CartGameViewModel>>(cookie); // Deserialize the cookie value to a List of CartGameViewModel objects
        }

        public IActionResult Summary()
        {
            List<CartGameViewModel> cartGames = GetExistingCartData(); // Get the current list of games in the shopping cart

            return View(cartGames); // Return the Summary view with the cartGames list
        }

        public IActionResult Remove(int id)
        {
            List<CartGameViewModel> cartGames = GetExistingCartData(); // Get the current list of games in the shopping cart

            CartGameViewModel? gameToRemove = cartGames.Where(g => g.GameId == id).FirstOrDefault(); // Get the game from the cartGames list with the specified id

            cartGames.Remove(gameToRemove); // Remove the game from the cartGames list

            WriteShoppingCartCookie(cartGames); // Write the updated cartGames list to the cookie

            return RedirectToAction("Summary"); // Redirect to the Summary action method
        }
    }
}
