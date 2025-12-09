using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AirBB.Models.DomainModels;
using AirBB.Models.ViewModels;
using AirBB.Models.DataLayer;

namespace AirBB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ValidationController : Controller
    {
        private readonly AirBBContext _context;
        public ValidationController(AirBBContext context) => _context = context;

        [AcceptVerbs("Get", "Post")]
        public JsonResult CheckEmail(string email)
        {
            var exists = _context.AppUsers.Any(u => u.Email == email);
            return exists
                ? Json($"The email {email} is already taken.")
                : Json(true);
        }
    }
}
