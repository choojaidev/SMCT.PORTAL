using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class UnAuthorizeController : Controller
    {
        // GET: UnAuthorizeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UnAuthorizeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UnAuthorizeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnAuthorizeController/Create
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

        // GET: UnAuthorizeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UnAuthorizeController/Edit/5
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

        // GET: UnAuthorizeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UnAuthorizeController/Delete/5
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
