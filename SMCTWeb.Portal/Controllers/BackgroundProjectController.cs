using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class BackgroundProjectController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
