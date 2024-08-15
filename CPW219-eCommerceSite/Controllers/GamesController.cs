using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGameContext _context;
        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method displays the form to create a new game. It's a GET request, where the server sends the form to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method handles the form submission when the user submits the data to create a new game.
        /// It's a POST request, where the data is sent to the server. Handles form submission, validates data,
        /// and either saves the game or returns the form with validation errors.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost] 
        public async Task<IActionResult> Create(Game game) // Model binding, this is a model object representing the game data submitted by the user. task is a type of object that represents an asynchronous operation.
        {
            if (ModelState.IsValid)
            {
                // Add the game to the database
                _context.Games.Add(game); // Prepare the game to be saved to the database
                // For async code information, see https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
                await _context.SaveChangesAsync(); // Save the game to the database. This is an async operation, so we use the await keyword. Add Async to the end of the method name.

                // show the user a success message
                ViewData["Message"] = $"{game.Title} was successfully added!";
                return View();
            }
            return View(game);
        }
    }
}
