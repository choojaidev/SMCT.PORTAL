using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using SMCTPortal.DataAccess.DatabaseContext;
using SMCTPortal.Model;
using SMCTPortal.Repository;
using System;

namespace SMCTPortal.Controllers
{
    public class FamilysController : Controller
    {

        private readonly MongoRepository _mongoRepository; 
        private readonly SMCTDbContext _context;
        private readonly MongoDBContext _mongoDBContext;
        // GET: CitizenInfoController
        private readonly IMongoCollection<BsonDocument> _citizen;
        // GET: FamilysController
        public FamilysController(MongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            _citizen = database.GetCollection<BsonDocument>("Citizens");
        }

        public ActionResult Index()
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Empty;
                var citizens = _citizen.Find(filter).ToList();

                // Convert MongoDB documents to dynamic objects
                var dynamicCitizen = new JArray();
                foreach (var ct in citizens)
                {
                    dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                }

                ViewBag.Citizens = dynamicCitizen;
                //     return View(data);
            }
            catch (Exception ex)
            {
                var xx = ex;
            }
            return View();
        }

        // GET: FamilysController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FamilysController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilysController/Create
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

        // GET: FamilysController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FamilysController/Edit/5
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

        // GET: FamilysController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FamilysController/Delete/5
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
