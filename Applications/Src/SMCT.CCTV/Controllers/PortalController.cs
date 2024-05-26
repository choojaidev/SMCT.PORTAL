//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Threading.Tasks;
//using Clients;
//using IdentityModel.Client;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authorization;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Clients;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SMCTWebTemplate.Model;
namespace SMCTWebTemplate.Controllers
{
    [AllowAnonymous]
    public class PortalController : Controller
    {
        // GET: PortalController
        public ActionResult Index()
        {
            return View("IndexMBGrid");
        }

        // GET: PortalController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PortalController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PortalController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: PortalController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PortalController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: PortalController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PortalController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public IActionResult Logout() => SignOut("oidc", "Cookies");
    }
}
