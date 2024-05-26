using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class CCTVMapController : Controller
    {
        public IActionResult Index()
        {
            return View("IndexMap");
        }
    }
}
