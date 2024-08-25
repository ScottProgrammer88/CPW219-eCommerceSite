using CPW219_eCommerceSite.Data;
using CPW219_eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_eCommerceSite.Controllers
{
    public class MembersController : Controller
    {
        private readonly VideoGameContext _context; // This is a private field that holds a reference to the VideoGameContext object. This object is used to interact with the database. Its accessible anywhere in this class.

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

                LogUserIn(regModel.Email); // This method stores the email in the session object. This will allow the application to remember the email of the user who is logged in.

                // Finally, the method redirects the user to the Login/Add/Home page.
                return RedirectToAction("Add", "Home");
            }

            // If the data is not valid, the method will return the Register view with the RegisterViewModel object, which contains the data entered by the user.
            return View(regModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the email and password match a Member in the database 
                // _context.Members.FirstOrDefaultAsync(m => m.Email == loginModel.Email && m.Password == loginModel.Password); same as below???
                Member? m = (from member in _context.Members
                           where member.Email == loginModel.Email && member.Password == loginModel.Password
                           select member).SingleOrDefault();

                // If a Member is found, send to Home page
                if (m != null)
                {
                    LogUserIn(loginModel.Email); // This method stores the email in the session object. This will allow the application to remember the email of the user who is logged in.
                    return RedirectToAction("Add", "Home");
                }
                // This method adds an error message to the ModelState object. This message will be displayed in the view if the email and password do not match a Member in the database.
                ModelState.AddModelError(string.Empty, "Credentials not found");
            }

            // If the email and password do not match a Member in the database, or ModelState is invalid.
            return View(loginModel);
        }

        private void LogUserIn(string email) // This method stores the email of the logged-in user in the session object. This will allow the application to keep track of the user's login status.
        {
            HttpContext.Session.SetString("Email", email);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // This method clears the session object. This will log the user out of the application.
            return RedirectToAction("Add", "Home");
        }
    }
}
