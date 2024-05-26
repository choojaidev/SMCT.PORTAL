using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class CCTVController : Controller
    {
        // GET: CCTVController
        public ActionResult Index()
        {
            return View("Index");
        }

        // GET: CCTVController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CCTVController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CCTVController/Create
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

        // GET: CCTVController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CCTVController/Edit/5
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

        // GET: CCTVController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CCTVController/Delete/5
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
