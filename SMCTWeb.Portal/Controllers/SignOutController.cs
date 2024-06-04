using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SMCTPortal.Controllers
{
    public class SignOutController : Controller
    {
        // GET: SignOutController
        public ActionResult Index() => SignOut("oidc", "Cookies");
        
        // GET: SignOutController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SignOutController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SignOutController/Create
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

        // GET: SignOutController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SignOutController/Edit/5
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

        // GET: SignOutController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SignOutController/Delete/5
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
