using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;

namespace TWSA.Controllers
{
    public class AccountController : Controller
    {
        // Displays the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handles login form submission
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (UserData.ValidateUser(username, password))
            {
                // Redirect to some protected page
                return RedirectToAction("ManageAccount");
            }

            // Set error message if login fails
            ViewData["ErrorMessage"] = "Invalid username or password.";
            return View();
        }

        // Displays the registration form
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Handles registration form submission
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                UserData.AddUser(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }

        // Displays the manage account form
        [HttpGet]
        public IActionResult ManageAccount()
        {
            return View();
        }

        // Handles manage account form submission
        [HttpPost]
        public IActionResult ManageAccount(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = UserData.GetUser(user.Username);
                if (existingUser != null)
                {
                    existingUser.FirstName = user.FirstName;
                    existingUser.Surname = user.Surname;
                    existingUser.Age = user.Age;
                    existingUser.City = user.City;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    UserData.AddUser(existingUser); // Update user data
                    return RedirectToAction("ManageAccount");
                }
            }
            return View(user);
        }
    }
}