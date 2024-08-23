using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGameContext _context;
        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? id) // Pass int id as a parameter to the Index method to get the page number from the query string
        {
            const int NumGamesToDisplayPerPage = 3; // Number of games to display on the home page
            const int PageOffset = 1; // Page offset for current page number and figuring out how many games to skip

            int currPage = id ?? 1; // Get the current page number from the query string. If the query string is null, set the current page number to 1.

            int totalNumOfProducts = await _context.Games.CountAsync(); // Get the total number of games in the database
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / NumGamesToDisplayPerPage); // Calculate the total number of pages
            int totalPages = Convert.ToInt32(maxNumPages); // Round the total number of pages to the nearest whole number

            // Commented out the method syntax version, same code below as query syntax
            // List<Game> games = await _context.Games.ToListAsync(); // Get all games from the database and store them in a list

            List<Game> games = await (from game in _context.Games
                                      select game)
                                      .Skip(NumGamesToDisplayPerPage * (currPage - PageOffset)) // Skip the games that are on previous pages
                                      .Take(NumGamesToDisplayPerPage) // Take the games for the current page
                                      .ToListAsync(); // Get the games for the current page

            GameCatalogViewModel catalogModel = new GameCatalogViewModel(games, totalPages, currPage); // Create a new GameCatalogViewModel object with the list of games, total number of pages, and current page number
            return View(catalogModel); // Pass the list of games to the view
        }

        /// <summary>
        /// This method displays the form to create a new gameToEdit. It's a GET request, where the server sends the form to the client.
        /// </summary>
        /// <returns></returns>
        [HttpGet] 
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This method handles the form submission when the user submits the data to create a new gameToEdit.
        /// It's a POST request, where the data is sent to the server. Handles form submission, validates data,
        /// and either saves the gameToEdit or returns the form with validation errors.
        /// </summary>
        /// <param name="game"></param>
        /// <returns></returns>
        [HttpPost] 
        public async Task<IActionResult> Create(Game game) // Model binding, this is a model object representing the gameToEdit data submitted by the user. task is a type of object that represents an asynchronous operation.
        {
            if (ModelState.IsValid)
            {
                // Add the gameToEdit to the database
                _context.Games.Add(game); // Prepare the gameToEdit to be saved to the database
                // For async code information, see https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/
                await _context.SaveChangesAsync(); // Save the gameToEdit to the database. This is an async operation, so we use the await keyword. Add Async to the end of the method name.

                // show the user a success message
                ViewData["Message"] = $"{game.Title} was successfully added!";
                return View();
            }
            return View(game);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Game gameToEdit = await _context.Games.FindAsync(id); // Find the gameToEdit with the specified id

            if (gameToEdit == null) // If the gameToEdit is not found in the database
            {
                return NotFound();  // or: RedirectToAction("RegisterViewModel"); Redirect the user to the RegisterViewModel action. 
            }
            
            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel) 
        {
            if (ModelState.IsValid) 
            {
                _context.Games.Update(gameModel); // Update the gameToEdit in the database. This creates an SQL UPDATE statement, but doesn't execute it yet.
                await _context.SaveChangesAsync(); // Execute the SQL UPDATE statement. This saves the changes to the database.

                TempData["Message"] = $"{gameModel.Title} was successfully updated!";
                return RedirectToAction("RegisterViewModel"); // Redirect the user to the RegisterViewModel action.
            }
            return View(gameModel);

        }

        public async Task<IActionResult> Delete(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id); // Find the gameToEdit with the specified id

            if (gameToDelete == null) // If the gameToEdit is not found in the database
            {
                return NotFound();  // 404 error 
            }

            return View(gameToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            Game gameToDelete = await _context.Games.FindAsync(id); // Execute the SQL DELETE statement. This removes the gameToEdit from the database.

            if (gameToDelete != null)
            {
                _context.Games.Remove(gameToDelete); // Remove the gameToEdit from the database. This creates an SQL DELETE statement, but doesn't execute it yet.
                await _context.SaveChangesAsync(); // Execute the SQL DELETE statement. This removes the gameToEdit from the database.

                TempData["Message"] = gameToDelete.Title + " was successfully deleted!";
                return RedirectToAction("RegisterViewModel"); // Redirect the user to the RegisterViewModel action.
            }

            TempData["Message"] = "This game was already deleted!";
            return RedirectToAction("RegisterViewModel"); // had gamtToDelete in here and was causing an error
        }

        public async Task<IActionResult> Details(int id)
        {
            Game? gameDetails = await _context.Games.FindAsync(id); // Find the gameToEdit with the specified id. 

            if (gameDetails == null) // If the gameToEdit is not found in the database
            {
                return NotFound();  // 404 error 
            }

            return View(gameDetails);
        }
    }
}
