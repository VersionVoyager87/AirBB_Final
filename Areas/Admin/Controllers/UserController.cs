
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using AirBB.Models.DataLayer;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly AirBBContext _context;

        public UserController(AirBBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.AppUsers.OrderBy(u => u.Name).ToList();
            return View(users);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.UserTypes = AppUser.UserTypeDict;
            return View();
        }

        [HttpPost]
        public IActionResult Add(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _context.AppUsers.Add(user);
                _context.SaveChanges();
                TempData["message"] = "User added successfully.";
                return RedirectToAction("Index");
            }

            TempData["validationMessage"] = "Please fix the validation errors below.";
            ViewBag.UserTypes = AppUser.UserTypeDict;
            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.AppUsers.Find(id);
            ViewBag.UserTypes = AppUser.UserTypeDict;
            return View(user);
        }

        [HttpPost]
        public IActionResult Edit(AppUser user)
        {
            if (ModelState.IsValid)
            {
                _context.AppUsers.Update(user);
                _context.SaveChanges();
                TempData["message"] = "User updated successfully.";
                return RedirectToAction("Index");
            }

            TempData["validationMessage"] = "Please fix the validation errors below.";
            ViewBag.UserTypes = AppUser.UserTypeDict;
            return View(user);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var user = _context.AppUsers.Find(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Delete(AppUser user)
        {
            _context.AppUsers.Remove(user);
            _context.SaveChanges();
            TempData["message"] = "User deleted.";
            return RedirectToAction("Index");
        }
    }
}