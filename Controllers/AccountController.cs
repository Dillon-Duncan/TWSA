using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace TWSA.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Displays the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Handles login form submission
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                HttpContext.Session.SetString("LoggedInUser", username);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                TempData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Index", "Home");
            }

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
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("LoggedInUser", user.Username);
                HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Index", "Home");
            }
            return View(user);
        }

        // Displays the manage account form
        [HttpGet]
        public async Task<IActionResult> ManageAccount()
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (username == null)
            {
                return RedirectToAction("Login");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return View(user);
        }

        // Handles manage account form submission
        [HttpPost]
        public async Task<IActionResult> ManageAccount(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _context.Users.FindAsync(user.UserId);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.FirstName = user.FirstName;
                existingUser.Surname = user.Surname;
                existingUser.Age = user.Age;
                existingUser.City = user.City;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;

                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Account updated successfully!";
                return RedirectToAction("ManageAccount");
            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
