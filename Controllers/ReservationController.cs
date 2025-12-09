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
    public class ReservationController : Controller
    {
        private readonly AirBBContext context;

        public ReservationController(AirBBContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index()
        {
            var session = new AirBBSession(HttpContext.Session);
            var reservations = session.GetReservations(context) ?? new List<Reservation>();

            return View(reservations);
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            var session = new AirBBSession(HttpContext.Session);
            var cookies = new AirBBCookies(Request, Response);

            // Remove from DB
            var reservation = context.Reservations?
                .FirstOrDefault(r => r.ReservationId == id);

            if (reservation != null)
            {
                context.Reservations!.Remove(reservation);
                context.SaveChanges();
            }

            // Remove from session
            var reservations = session.GetReservations(context) ?? new List<Reservation>();
            var updated = reservations.Where(r => r.ReservationId != id).ToList();
            session.SetReservations(updated);

            // Update cookie
            cookies.SetReservationIds(updated);

            TempData["message"] = "Reservation cancelled.";
            return RedirectToAction("Index");
        }
    }
}
