using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly VideoGameContext _context;

        public MembersController(VideoGameContext context)
        {
            _context = context;
        }

        [HttpGet] // This is always here by default. This attribute specifies that the Register method should only respond to GET requests.
                  // This means that the method will only be called when the user navigates to the Register page. When they send data using a form, that is a HttpPost 
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost] // This attribute specifies that the Register method should only respond to POST requests. This means that the method will only be called when the user submits the registration form.
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid) // This method checks if the data entered by the user is valid according to the validation rules specified in the RegisterViewModel class.
            {
                // Map the RegisterViewModel to a Member, Map RegisterViewModel data to a Member object.
                // If the data is valid, the method will create a new Member object and set its properties to the values entered by the user.
                // This will create a new Member object with the email and password entered by the user, and will be added to the database, with the SaveChanges method, below.
                Member newMember = new Member
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                // The new Member object is then added to the database using the Add method of the DbContext object.
                _context.Members.Add(newMember);  // This is the same as INSERT INTO Members (Email, Password) VALUES (regModel.Email, regModel.Password)
                await _context.SaveChangesAsync();  // This method saves the changes to the database.

                // Finally, the method redirects the user to the Login/Index/Home page.
                return RedirectToAction("Index", "Home");
            }

            // If the data is not valid, the method will return the Register view with the RegisterViewModel object, which contains the data entered by the user.
            return View(regModel);
        }
    }
}
