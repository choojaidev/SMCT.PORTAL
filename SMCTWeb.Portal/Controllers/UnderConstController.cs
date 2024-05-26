using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class UnderConstController : Controller
    {
        // GET: UnderConstController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UnderConstController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UnderConstController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnderConstController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UnderConstController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UnderConstController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UnderConstController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UnderConstController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
