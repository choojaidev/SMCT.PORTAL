using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class CitizenV2Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
