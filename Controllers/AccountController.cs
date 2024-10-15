using Microsoft.AspNetCore.Mvc;
using TWSA.Data;
using TWSA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System.Threading.Tasks;

namespace TWSA.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("LoggedInUser") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Username and password are required.");
                return View();
            }

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    HttpContext.Session.SetString("LoggedInUser", username);
                    HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                    TempData["SuccessMessage"] = "Login successful!";
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password.");
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (HttpContext.Session.GetString("LoggedInUser") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username || u.Email == user.Email);
                    if (existingUser != null)
                    {
                        ModelState.AddModelError("", "Username or email already exists.");
                        return View(user);
                    }

                    user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("LoggedInUser", user.Username);
                    HttpContext.Session.SetString("IsAdmin", user.IsAdmin.ToString());
                    TempData["SuccessMessage"] = "Registration successful!";
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while processing your request. Please try again.");
                    return View(user);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> ManageAccount()
        {
            var username = HttpContext.Session.GetString("LoggedInUser");
            if (username == null)
            {
                return RedirectToAction("Login");
            }

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving your account information. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ManageAccount(User user)
        {
            if (ModelState.IsValid)
            {
                try
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while updating your account. Please try again.");
                    return View(user);
                }
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
