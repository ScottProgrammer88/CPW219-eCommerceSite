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

        public async Task<IActionResult> Index()
        {
            List<Game> games = await _context.Games.ToListAsync(); // Get all games from the database and store them in a list

            return View(games); // Pass the list of games to the view
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
                return NotFound();  // or: RedirectToAction("Index"); Redirect the user to the Index action. 
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
                return RedirectToAction("Index"); // Redirect the user to the Index action.
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
                return RedirectToAction("Index"); // Redirect the user to the Index action.
            }

            TempData["Message"] = "This game was already deleted!";
            return RedirectToAction("Index"); // had gamtToDelete in here and was causing an error
        }
    }
}
