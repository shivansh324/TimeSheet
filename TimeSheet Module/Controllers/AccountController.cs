using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using TimeSheet.Data.Data;
using TimeSheet.Models;
using TimeSheet.Models.ViewModel;

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
            Employee? employee = _db.Employees.Include(x => x.Department).FirstOrDefault(x => x.EmployeeCode == UserId);
            if (employee != null && VerifyPassword(Password, employee.Password) && employee.Status=="Active")
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, employee.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, employee.Name.ToString()),
                    new Claim(ClaimTypes.Role, employee.Role.ToString())
                };
                var identity = new ClaimsIdentity(claims, "CookieAuthentication");
                var principal = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync("CookieAuthentication", principal);
                TempData["success"] = "Login successfully!!";
                return RedirectToAction("Index", "Home");
            }
            TempData["error"] = "Invalid UserId or Password";
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("CookieAuthentication");
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Employees()
        {
            var employees = _db.Employees.Include(x => x.Department).Include(x => x.Approver).ToList();
            return View(employees);
        }

        public IActionResult Register()
        {
            ViewBag.Departments = _db.Departments.ToList();
            ViewBag.Approvers = _db.Employees.OrderBy(x => x.EmployeeCode).ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Register(EmployeeVM employee)
        {
            if (ModelState.IsValid)
            {
                if (_db.Employees.Any(x => x.EmployeeCode == employee.EmployeeCode))
                {
                    ModelState.AddModelError("EmployeeCode", "Employee Code already exists.");
                    ViewBag.Departments = _db.Departments.ToList();
                    ViewBag.Approvers = _db.Employees.OrderBy(x => x.EmployeeCode).ToList();
                    return View(employee);
                }
                Employee emp = new()
                {
                    EmployeeCode = employee.EmployeeCode,
                    Name = employee.Name,
                    Email = employee.Email?.ToString(),
                    DepartmentId = employee.DepartmentId,
                    ApproverId = employee.ApproverId,
                    Status = employee.Status,
                    Password = HashPassword(employee.Password)
                };
                _db.Employees.Add(emp);
                _db.SaveChanges();
                TempData["success"] = "Employee registered successfully!";
                return RedirectToAction("Employees");
            }
            ViewBag.Departments = _db.Departments.ToList();
            ViewBag.Approvers = _db.Employees.OrderBy(x => x.EmployeeCode).ToList();
            return View(employee);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEmp(int id, int status)
        {
            if (id == int.Parse(User?.FindFirst(ClaimTypes.Name).Value)) // Prevent deletion of the logged-in user
            {
                return StatusCode(400, new { error = "You cannot delete or disable your own account!" });
            }
            Employee? employee = _db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            if (status == 1)
            {
                _db.Employees.Remove(employee);
            }
            else
            {
                employee.Status = "Inactive";
                _db.Employees.Update(employee);
            }
            _db.SaveChanges();
            TempData["success"] = "Employee updated successfully!";
            return Ok();
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 32);

            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);
            byte[] newHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 32);

            return hash.SequenceEqual(newHash);
        }

    }
}
