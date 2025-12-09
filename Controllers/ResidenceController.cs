using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using AirBB.Models;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using AirBB.Models.DataLayer;
using AirBB.Models.Utilities;

namespace AirBB.Controllers
{
    public class ResidenceController : Controller
    {
        private readonly AirBBContext context;

        public ResidenceController(AirBBContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Details(int id)
        {
            var session = new AirBBSession(HttpContext.Session);
            var model = new ResidenceViewModel
            {
                Criteria = session.GetFilter() ?? new(),
                Residence = context.Residences?
                    .Include(r => r.Location)
                    .FirstOrDefault(r => r.ResidenceId == id) ?? new Residence()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Reserve(int id)
        {
            var session = new AirBBSession(HttpContext.Session);
            var cookies = new AirBBCookies(Request, Response);

            var residence = context.Residences?
                .Include(r => r.Location)
                .FirstOrDefault(r => r.ResidenceId == id);

            if (residence == null)
                return RedirectToAction("Index", "Home");

            // Create new reservation
            var reservation = new Reservation
            {
                ReservationId = context.Reservations?.Any() == true
                    ? context.Reservations.Max(r => r.ReservationId) + 1
                    : 1,
                ResidenceId = residence.ResidenceId,
                Residence = residence,
                ReservationStartDate = DateTime.Today,
                ReservationEndDate = DateTime.Today.AddDays(2)
            };

            context.Reservations?.Add(reservation);
            context.SaveChanges();

            // Get reservations from session (or empty if null)
            var reservations = session.GetReservations(context) ?? new List<Reservation>();

            // Add the new reservation
            reservations.Add(reservation);
            session.SetReservations(reservations);

            // Save only reservation IDs to cookie
            cookies.SetReservationIds(reservations);

            TempData["message"] = $"{residence.Name} reserved successfully!";
            return RedirectToAction("Index", "Home"); // PRG pattern
        }
    }
}
