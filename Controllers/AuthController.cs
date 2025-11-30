using Microsoft.AspNetCore.Mvc;
using WebApplication16.Data;
using WebApplication16.Data.Models;
using WebApplication16.Services;

namespace WebApplication16.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IPasswordService _passwordService;

        public AuthController(AppDbContext context, IPasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
            if (_context.Users.Any(u => u.Email == email))
            {
                ModelState.AddModelError("", "Email already used");
                return View();
            }

            var user = new User
            {
                Username = username,
                Email = email,
                HashPassword = _passwordService.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Document");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null || !_passwordService.VerifyPassword(user.HashPassword, password))
            {
                ModelState.AddModelError("", "Invalid credentials");
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.Id);
            return RedirectToAction("Index", "Document");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
