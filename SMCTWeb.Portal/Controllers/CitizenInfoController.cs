using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMCTPortal.Model;
using SMCTPortal.Repository;
using System;
 
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using MongoDB.Bson;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using SMCTPortal.Model.SMPeople;
using System.Linq;
using System.Text.Json;
namespace SMCTPortal.Controllers
{
   
    public class CitizenInfoController : Controller
    {

        private readonly IMongoClient _mongoClient;
        private readonly MongoRepository _mongoRepository;
        private readonly SMCTDbContext _context;
        private readonly MongoDBContext _mongoDBContext;
        // GET: CitizenInfoController
        private readonly IMongoCollection<tbPeople> _citizen;
        public CitizenInfoController(MongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            _citizen = database.GetCollection<tbPeople>("Citizens");
        }

        clsutil _uti = new clsutil();
        public  ActionResult Index()
        {
            var uid = User.Claims;
            var xid = (from cc in User.Claims
                       where cc.Type.ToString().IndexOf("sub") > -1
                       select cc).ToList();
            var roleInfo = (from cc in User.Claims
							where cc.Type.ToString().IndexOf("role") > -1
							select cc).ToList();
			try
            {//
             // var filter = Builders<BsonDocument>.Filter.Empty;
                string xx = xid[0].Value.ToString();
                var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());
                var citizens = _citizen.Find(filter).ToList();

                // Convert MongoDB documents to dynamic objects
                var dynamicCitizen = new JArray();
                foreach (var ct in citizens)
                {
                    dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                }


                //ViewBag.Citizens = dynamicCitizen;
                tbPeople data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                if (data.family == null) data.family = [];
             
                return View("IndexMobi", data);
            }
            catch(Exception ex)
            {
                var xx = ex;
            }
			tbPeople dataexist = new tbPeople();
            if (dataexist.citizenNo == null) dataexist.citizenNo = xid[0].Subject.Name.ToString();
            if (dataexist.Name == null) dataexist.Name = "";
            if (dataexist.SureName == null) dataexist.SureName = "";
			return View("IndexMobi",dataexist);
        }

        // GET: CitizenInfoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: CitizenInfoController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: CitizenInfoController/Create
      //  [HttpPost]
   //     [ValidateAntiForgeryToken]
        public ActionResult Create(tbPeople data)
        {
            // add new element to family
            //try
            //{
            //    DataAccess.database db = new DataAccess.database(_mongoClient);

            //    List<tbPeople > people = new List<tbPeople>();
            //    people.Add(new tbPeople { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), Name ="test1",SureName ="test2" });
            //    people.Add(new tbPeople { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), Name ="test3",SureName ="test3" });
            //  data.family = people;
            //    data.id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            //    var res=  db.SaveData (data);
            //    //return RedirectToAction(nameof(Index));
            //    return View(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}

            //try
            //{
            //    var uid = User.Claims;
            //    var xid = (from cc in User.Claims
            //               where cc.Type.ToString().IndexOf("sub") > -1
            //               select cc).ToList();
            //    string xx = xid[0].Value.ToString();
            //    //    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());
            //    DataAccess.database db = new DataAccess.database(_mongoClient);
            //    data.citizenNo = xid[0].Subject.Name.ToString();
            //    db.UpdateFamilyData(data);
            //    return View(nameof(Index), data);
            //}
            //catch
            //{
            //    return View();
            //}
            var uid = User.Claims;
            var xid = (from cc in User.Claims
                       where cc.Type.ToString().IndexOf("sub") > -1
                       select cc).ToList();
            try
            {//
             // var filter = Builders<BsonDocument>.Filter.Empty;
                string xx = xid[0].Value.ToString();
                var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());
                var citizens = _citizen.Find(filter).ToList();

                // Convert MongoDB documents to dynamic objects
                var dynamicCitizen = new JArray();
                foreach (var ct in citizens)
                {
                    dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                }


                //ViewBag.Citizens = dynamicCitizen;
                tbPeople existData = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                List<tbPeople> lsFam = new List<tbPeople>();
                try {

                    foreach (var fa in existData.family) { lsFam.Add(fa); }
                } catch (Exception ex) { }
                data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                data.ParentId = existData.citizenNo;
                data.createDate = _uti.getSysDate(true);
                data.updateDate = _uti.getSysDate(true);
                lsFam.Add (data);  
                existData .family = lsFam;


                DataAccess.database db = new DataAccess.database(_mongoClient);
                db.UpdateFamilyData(existData);
                return RedirectToAction(nameof(Index),existData);
            }
            catch (Exception ex)
            {
                var xx = ex;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: CitizenInfoController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: CitizenInfoController/Edit/5
      // [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult Edit(tbPeople data)
        {
            try
            {
                //    var client = new MongoClient("mongodb://localhost:27017");
                //    var database = client.GetDatabase("SMZT");

                //    var collection = database.GetCollection<tbPeople>("Citizens");
                //    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", data.citizenNo);

                //    var uid = User.Claims;
                //    var xid = (from cc in User.Claims
                //               where cc.Type.ToString().IndexOf("sub") > -1
                //               select cc).ToList();
                //    string xx = xid[0].Value.ToString();
                ////    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());
                //    DataAccess.database db = new DataAccess.database(_mongoClient);

                //    try {

                //        // tbPeople existing = JsonSerializer.Deserialize<tbPeople>(data.Root[0].ToString());
                //    } catch (Exception ex) { }

                var uid = User.Claims;
                var xid = (from cc in User.Claims
                           where cc.Type.ToString().IndexOf("sub") > -1
                           select cc).ToList();
                try
                {//
                 // var filter = Builders<BsonDocument>.Filter.Empty;
                    string xx = xid[0].Value.ToString();
                    //   var filter = Builders<tbPeople>.Filter.Eq("_id", data._id);
                    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());

                    var citizens = _citizen.Find(filter).ToList();

                    // Convert MongoDB documents to dynamic objects
                    var dynamicCitizen = new JArray();
                    foreach (var ct in citizens)
                    {
                        dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                    }


                    //ViewBag.Citizens = dynamicCitizen;
                    tbPeople existData = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<tbPeople> lsFam = new List<tbPeople>();
                    try
                    {

                        foreach (var fa in existData.family) {                         
                            if(fa._id != data._id )lsFam.Add(fa);                         
                        }
                    }
                    catch (Exception ex) { }
                   // tbPeople editData = JsonSerializer.Deserialize<tbPeople>()
                   // data._id =  Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    data.ParentId = existData.citizenNo;                 
                    data.updateDate = _uti.getSysDate(true);
                    lsFam.Add(data);
                    existData.family = lsFam;


                    DataAccess.database db = new DataAccess.database(_mongoClient);
                    db.UpdateFamilyData(existData);
                    return RedirectToAction(nameof(Index), existData);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
                
                return RedirectToAction(nameof(Index),data);
            }
            catch
            {
                return View();
            }
        }

        // GET: CitizenInfoController/Delete/5
        public ActionResult Delete(tbPeople data)
        {
            try
            {
                

                var uid = User.Claims;
                var xid = (from cc in User.Claims
                           where cc.Type.ToString().IndexOf("sub") > -1
                           select cc).ToList();
                try
                {//
                 // var filter = Builders<BsonDocument>.Filter.Empty;
                    string xx = xid[0].Value.ToString();
                    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());
                    var citizens = _citizen.Find(filter).ToList();

                    // Convert MongoDB documents to dynamic objects
                    var dynamicCitizen = new JArray();
                    foreach (var ct in citizens)
                    {
                        if(ct._id != data._id) dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                    }


                    //ViewBag.Citizens = dynamicCitizen;
                    tbPeople existData = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<tbPeople> lsFam = new List<tbPeople>();
                    try
                    {

                        foreach (var fa in existData.family)
                        {

                            if (fa._id  != data._id ) lsFam.Add(fa);

                        }
                    }
                    catch (Exception ex) { }
                    // tbPeople editData = JsonSerializer.Deserialize<tbPeople>()
                   // data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                   // data.ParentId = existData.citizenNo;
                  //  lsFam.Add(data);
                    existData.family = lsFam;


                    DataAccess.database db = new DataAccess.database(_mongoClient);
                    db.UpdateFamilyData(existData);
                    return RedirectToAction(nameof(Index), existData);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }

                return RedirectToAction(nameof(Index), data);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("IndexMobi");
        }

        // POST: CitizenInfoController/Delete/5
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
    }
 
}
