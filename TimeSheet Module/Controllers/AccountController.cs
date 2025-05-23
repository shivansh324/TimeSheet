using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;

namespace TimeSheet_Module.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string UserId, string Password)
        {
            Employee? employee = _db.Employees.Include(x=>x.Department).FirstOrDefault(x=>x.Id == int.Parse(UserId));
            if (employee == null)
            {
                TempData["error"] = "Invalid UserId or Password";
                return View();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, UserId),
                new Claim(ClaimTypes.NameIdentifier, employee.Name.ToString()),
                new Claim(ClaimTypes.Role, employee.Department.Name.ToString())
            };
            var identity = new ClaimsIdentity(claims, "CookieAuthentication");
            var principal = new ClaimsPrincipal(identity);
            HttpContext.SignInAsync("CookieAuthentication", principal);
            TempData["success"] = "Login successfully!!";
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuthentication");
            //HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
