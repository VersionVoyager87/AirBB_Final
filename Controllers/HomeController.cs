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
    public class HomeController : Controller
    {
        private readonly AirBBContext _context;

        public HomeController(AirBBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var session = new AirBBSession(HttpContext.Session);
            var cookies = new AirBBCookies(Request, Response);

            var criteria = session.GetFilter() ?? new FilterCriteria();
            var reservations = session.GetReservations(_context);

            // Fallback: load reservations from cookies if session is empty
            if (!reservations.Any())
            {
                var ids = cookies.GetReservationIds() ?? new List<int>();

                if (ids.Any())
                {
                    reservations = _context.Reservations!
                        .Include(r => r.Residence)
                            .ThenInclude(res => res!.Location)
                        .Where(r => ids.Contains(r.ReservationId))
                        .ToList();

                    session.SetReservations(reservations);
                }
            }

            // Make context available to _Layout for badge count
            ViewData["Context"] = _context;

            var model = new ResidenceViewModel
            {
                Criteria = criteria,
                Locations = _context.Locations?.OrderBy(l => l.Name).ToList() ?? new List<Location>(),
                Residences = GetFilteredResidences(criteria),
                Reservations = reservations,
                Message = TempData["message"] as string ?? ""
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(FilterCriteria criteria)
        {
            var session = new AirBBSession(HttpContext.Session);
            session.SetFilter(criteria);
            return RedirectToAction("Index"); // ✅ PRG pattern
        }

        private List<Residence> GetFilteredResidences(FilterCriteria c)
        {
            if (_context.Residences == null) return new List<Residence>();

            var query = _context.Residences
                .Include(r => r.Location)
                .AsQueryable();

            // Where filter
            if (!string.IsNullOrEmpty(c.ActiveLocation) && c.ActiveLocation.ToLower() != "all")
            {
                query = query.Where(r =>
                    r.Location != null &&
                    r.Location.Name.ToLower() == c.ActiveLocation.ToLower());
            }

            // When filter
            if (!string.IsNullOrEmpty(c.StartDate) && !string.IsNullOrEmpty(c.EndDate))
            {
                if (DateTime.TryParse(c.StartDate, out DateTime start) &&
                    DateTime.TryParse(c.EndDate, out DateTime end))
                {
                    var reservedIds = _context.Reservations?
                        .Where(r =>
                            r.ReservationStartDate <= end &&
                            r.ReservationEndDate >= start)
                        .Select(r => r.ResidenceId)
                        .Distinct()
                        .ToList() ?? new List<int>();

                    query = query.Where(r => !reservedIds.Contains(r.ResidenceId));
                }
            }

            // Who filter
            if (c.GuestCount > 0)
            {
                query = query.Where(r => r.GuestNumber >= c.GuestCount);
            }

            return query.OrderBy(r => r.Name).ToList();
        }
    }
}
