using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using AirBB.Models.DataLayer;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LocationController : Controller
    {
        private readonly AirBBContext _context;

        public LocationController(AirBBContext ctx) => _context = ctx;

        public IActionResult Index()
        {
            var locations = _context.Locations
                .OrderBy(l => l.Name)
                .ToList();

            return View(locations);
        }

        [HttpGet]
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Add(location);
                _context.SaveChanges();

                TempData["message"] = "Location added successfully.";
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var loc = _context.Locations.Find(id);
            return View(loc);
        }

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _context.Locations.Update(location);
                _context.SaveChanges();

                TempData["message"] = "Location updated.";
                return RedirectToAction("Index");
            }
            return View(location);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var loc = _context.Locations.Find(id);
            return View(loc);
        }

        [HttpPost]
        public IActionResult Delete(Location location)
        {
            _context.Locations.Remove(location);
            _context.SaveChanges();

            TempData["message"] = "Location deleted.";
            return RedirectToAction("Index");
        }
    }
}
