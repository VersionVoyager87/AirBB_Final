using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using AirBB.Models.DataLayer;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ResidenceController : Controller
    {
        private readonly AirBBContext _context;

        public ResidenceController(AirBBContext context)
        {
            _context = context;
        }

        // ---------- INDEX ----------
        public IActionResult Index()
        {
            var residences = _context.Residences
                .Include(r => r.Location)
                .OrderBy(r => r.Name)
                .ToList();

            return View(residences);
        }

        // ---------- ADD ----------
        [HttpGet]
        public IActionResult Add()
        {
            var vm = LoadViewModel(new Residence());
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(ResidenceAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Residences.Add(vm.Residence);
                _context.SaveChanges();
                TempData["message"] = "Residence added successfully.";
                return RedirectToAction("Index");
            }

            TempData["validationMessage"] = "Please fix the errors below.";
            return View(LoadViewModel(vm.Residence));
        }

        // ---------- EDIT ----------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var residence = _context.Residences.Find(id);
            if (residence == null)
                return RedirectToAction("Index");

            var vm = LoadViewModel(residence);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(ResidenceAdminViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _context.Residences.Update(vm.Residence);
                _context.SaveChanges();
                TempData["message"] = "Residence updated successfully.";
                return RedirectToAction("Index");
            }

            TempData["validationMessage"] = "Please fix the errors below.";
            return View(LoadViewModel(vm.Residence));
        }

        // ---------- DELETE ----------
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var residence = _context.Residences
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null)
                return RedirectToAction("Index");

            return View(residence);
        }

        [HttpPost]
        public IActionResult Delete(Residence model)
        {
            _context.Residences.Remove(model);
            _context.SaveChanges();
            TempData["message"] = "Residence deleted.";
            return RedirectToAction("Index");
        }

        // ---------- HELPER ----------
        private ResidenceAdminViewModel LoadViewModel(Residence residence)
        {
            return new ResidenceAdminViewModel
            {
                Residence = residence,
                Locations = _context.Locations.OrderBy(l => l.Name).ToList(),
                Owners = _context.AppUsers
                    .Where(u => u.UserType == "Owner")
                    .OrderBy(u => u.Name)
                    .ToList()
            };
        }
    }
}
