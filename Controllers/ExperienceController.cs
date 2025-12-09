using Microsoft.AspNetCore.Mvc;

namespace AirBB.Models
{
    public class ExperienceController : Controller
    {
        
        public IActionResult List(string id = "All")
        {
            return Content($"Area=Public, Controller=Experience, Action=List, ID={id}");
        }

        
        public IActionResult Detail(int id)
        {
            return Content($"Area=Public, Controller=Experience, Action=Detail, ID={id}");
        }
    }
}
