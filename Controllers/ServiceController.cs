using Microsoft.AspNetCore.Mvc;

namespace AirBB.Controllers
{
    public class ServiceController : Controller
    {
        
        public IActionResult List(string id = "All")
        {
            return Content($"Area=Public, Controller=Service, Action=List, ID={id}");
        }

        
        public IActionResult Detail(int id)
        {
            return Content($"Area=Public, Controller=Service, Action=Detail, ID={id}");
        }
    }
}
