using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TimeSheet.Data.Data;
using TimeSheet.Models;

namespace TimeSheet_Module.Controllers
{
    public class AdminController(ApplicationDbContext db) : Controller
    {
        private readonly ApplicationDbContext _db = db;

        public async Task<IActionResult> Department()
        {
            return View(await _db.Departments.ToListAsync());
        }

        public async Task<IActionResult> Milestone()
        {
            ViewBag.Departments = await _db.Departments.OrderBy(x=>x.Name).ToListAsync();
            return View(await _db.Milestones.Include(x=>x.Department).ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Department(int Id, string Name)
        {
            if (ModelState.IsValid)
            {
                if (Id != 0)
                {
                    Department? department = await _db.Departments.FindAsync(Id);
                    if (department == null)
                    {
                        return NotFound();
                    }
                    department.Name = Name;
                    _db.Departments.Update(department);
                }
                else
                {
                    Department department = new Department
                    {
                        Name = Name
                    };
                    _db.Departments.Add(department);
                }
                await _db.SaveChangesAsync();
                TempData["success"] = "Department saved successfully";
                return RedirectToAction(nameof(Department));
            }
            return View(nameof(Department));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            if (department != null)
            {
                _db.Departments.Remove(department);
            }

            await _db.SaveChangesAsync();
            TempData["success"] = "Department deleted successfully";
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Milestone(int? Id,string Name, int DepartmentId, string Status)
        {
            if (ModelState.IsValid)
            {
                if (Id != 0)
                {
                    Milestone? milestone = await _db.Milestones.FindAsync(Id);
                    if (milestone == null)
                    {
                        return NotFound();
                    }
                    milestone.Name = Name;
                    milestone.DepartmentId = DepartmentId;
                    milestone.Status = Status;
                    _db.Milestones.Update(milestone);
                }
                else
                {
                    Milestone milestone = new Milestone
                    {
                        Name = Name,
                        DepartmentId = DepartmentId,
                        Status = Status
                    };
                    _db.Milestones.Add(milestone);
                }
                await _db.SaveChangesAsync();
                TempData["success"] = "Milestone saved successfully";
                return RedirectToAction(nameof(Milestone));
            }
            return RedirectToAction(nameof(Milestone));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMilestone(int id)
        {
            var milestone = await _db.Milestones.FindAsync(id);
            if (milestone != null)
            {
                _db.Milestones.Remove(milestone);
            }

            await _db.SaveChangesAsync();
            TempData["success"] = "Milestone deleted successfully";
            return Ok();
        }
    }
}
