using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMCTPortal.DataAccess.Repositories.Implementations;
using SMCTPortal.DataAccess.Repositories.Interfaces;
using SMCTPortal.Model;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Model.SMTCitizen;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using SMCTPortal.Repository;
using System.Threading.Tasks;
using SMCTPortal.DataAccess.DatabaseContext;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using SMCTPortal.Model.ViewModel;
namespace SMCTWebTemplate.Controllers
{
    public class Message {
        public string title { get; set; }
        public string text { get; set; }
        public string icon { get; set; }

    }

    public class SmartCitizenController : Controller
    {
        private readonly MongoRepository _mongoRepository;
        private readonly SMCTDbContext _context;
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<tbPeople> _citizen;
        public SmartCitizenController(SMCTDbContext context, MongoRepository mongoRepository)//, MongoDBContext mongoDBContext
        {
            _context = context;
            _mongoRepository = mongoRepository;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            _citizen = database.GetCollection<tbPeople>("Citizens");
        }


        // GET: SmartCityzenController
        // [Authorize]
        public async Task<ActionResult> Index(Message msg)
        {
          if(msg.text !="")  ViewBag.Msg = msg;
            tbPeople data = new tbPeople();
          
            List<mdEducation> lsEdu = new List<mdEducation>();
            lsEdu.Add(new mdEducation { id = "" });
            List<mdRelocation> lsRelo = new List<mdRelocation>();
            lsRelo.Add(new mdRelocation { id = "" });
            data.educationInfos = lsEdu;
            data.relocationInfos = lsRelo;
            //try {
            //        var data = await _mongoRepository.GetAllData();
            //        var xx = data;
            //        Console.WriteLine("value of dbmongo");
            //       // Console.WriteLine(xx);
            //        //var connectionString = Environment.GetEnvironmentVariable("mongodb://localhost:27017");
            //        //if (connectionString == null)
            //        //{
            //        //    Environment.Exit(0);
            //        //}
            //        //var client = new MongoClient(connectionString);
            //        //var collection = client.GetDatabase("SMZT").GetCollection<BsonDocument>("Citizens");
            //        //var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");
            //        //var document = collection.Find(filter).First();
            //        //Console.WriteLine(document);
            //        //// var usersCollection = _mongoDBContext.Citizens; // Assuming you have a Users property in your MongoDBContext

            //        // Retrieve all users
            //        // var users = usersCollection.Find(FilterDefinition<Citizens>.Empty).ToList();


            //        // var xx = ct;
            //    }
            //    catch(Exception ex) { }  
            //    try {
            //        var uid = User.Claims;
            //        var xid = (from cc in User.Claims
            //               where cc.Type.ToString().IndexOf("sub") > -1
            //                  select cc).ToList();
            //         var data = _context.tbPeople.FirstOrDefault(x => x.citizenNo == xid[0].Value.ToString ());
            //        if (data != null) {
            //            return View(data);
            //        }
            //    } catch (Exception ex) {
            //        var xx = ex.Message.ToString ();
            //    }
            //    tbPeople obj = new tbPeople();

            //    obj.citizenNo = "";
            //    obj.Name = "";
            
            var uid = User.Claims;
            var xid = (from cc in User.Claims
                       where cc.Type.ToString().IndexOf("sub") > -1
                       select cc).ToList();

            data.citizenNo = xid[0].Subject.Name.ToString();

            try
            {//
             // var filter = Builders<BsonDocument>.Filter.Empty;
             //string xx = xid[0].Value.ToString();
                var user_id = User.Identity.Name;
                var filter = Builders<tbPeople>.Filter.Eq("citizenNo", user_id);
                var citizens = _citizen.Find(filter).ToList();
                ViewBag.UserName = citizens[0].Name + " " + citizens[0].SureName;
                // Convert MongoDB documents to dynamic objects
                var dynamicCitizen = new JArray();
                foreach (var ct in citizens)
                {
                    dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                }


                //ViewBag.Citizens = dynamicCitizen;
                // tbPeople
                data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                if (data.educationInfos == null || data.educationInfos.Count == 0)
                {
                    List<mdEducation> edLs = new List<mdEducation>();
                    edLs.Add(new mdEducation { });
                    data.educationInfos = edLs;
                }
                if (data.relocationInfos == null || data.relocationInfos.Count == 0)
                {
                    List<mdRelocation> edLs = new List<mdRelocation>();
                    edLs.Add(new mdRelocation { });
                    data.relocationInfos = edLs;
                }

                return View(data);
                // return View("Index");
            }
            catch (Exception ex)
            {
                var xx = ex;
            }
            return View(data);
        }

        // GET: SmartCityzenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SmartCityzenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SmartCityzenController/Create
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

        // GET: SmartCityzenController/Edit/5

        //public ActionResult Edit(tbPeople data)
        //{


        //  string xx = data.Name;
        // // var pp = new tbPeople();
        //  try {

        //            var entities = _context.tbPeople.ToList();
        //        // Check if entities are retrieved successfully

        //        var dbPP = _context.tbPeople.FirstOrDefault(x => x.citizenNo == data.citizenNo);
        //            if (dbPP == null) {
        //            var newPP = new tbPeople();
        //            newPP.Name = data.Name;
        //            newPP.citizenNo = data.citizenNo;
        //            _context.tbPeople.Add(newPP);
        //            _context.SaveChanges();
        //        } else { 
        //         dbPP.Name = data.Name;
        //            dbPP.SureName = data.SureName;
        //            dbPP.DateOfBirth = data.DateOfBirth;
        //            dbPP.PhoneNo = data.PhoneNo;
        //            dbPP.Address = data.Address;
        //            dbPP.Email = data.Email;
        //          var ret = _context.SaveChanges();
        //        }

        //    }
        //    catch (Exception ex) {
        //        var newPP = new tbPeople();
        //        newPP.Name = data.Name;
        //        newPP.citizenNo  = data.citizenNo;
        //        _context.tbPeople.Add(newPP);
        //        _context.SaveChanges();
        //        return View("Index", newPP);
        //    }

        //    ViewBag.Message = "ขอบคุณสำหรับการอัพเดทข้อมูล";
        //    return View("Index",data);
        //}
         public ActionResult Edit(tbPeople data)
        {
            // tbPeople data = citizData.peopleInfo; 
         
            try {  
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
                    if (citizens.Count > 0)
                    {
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

                            //foreach (var fa in existData.family) { 

                            //    if(fa.citizenNo != data.citizenNo )lsFam.Add(fa); 

                            //}



                            SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                            clsutil _uti = new clsutil();

                            data.updateDate = _uti.getSysDate();
                            db.UpdateData(data);

                        }
                        catch (Exception ex) { }

                    }
                    else
                    {
                        // tbPeople editData = JsonSerializer.Deserialize<tbPeople>()
                        data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        //      data.ParentId = existData.citizenNo;
                        //       lsFam.Add(data);
                        //     existData.family = lsFam;
                        SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        clsutil _uti = new clsutil();
                        data.createDate = _uti.getSysDate();
                        data.updateDate = _uti.getSysDate();
                        db.SaveData(data);
                    }




                    var _msg = new Message ();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                  
                  //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
                    return RedirectToAction("Index",_msg);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var xx = ex;
                var _msg = new Message();
                _msg.title = "Info";
                _msg.text = "Error :" + ex.Message.ToString();
                _msg.icon = "error";
                return RedirectToAction("Index", new { msg = _msg});
            }
            //   ViewBag.Message = "ขอบคุณสำหรับการอัพเดทข้อมูล";
            
            //_msg.title = "informations";
            //_msg.text = "ขอบคุณสำหรับการอัพเดทข้อมูล";
            //_msg.icon = "";
            //ViewBag.msg = _msg;
            //ViewBag.SaveResult = "Update SuccessFull!";
            //return RedirectToAction("Index",ViewBag);// View("Index",data);
        }

        // POST: SmartCityzenController/Edit/5

        public ActionResult AddEdu(mdEducation newData)
        {
            if (newData.citizenNo != null)
            {
                tbPeople data = new tbPeople();
                //    data.citizenNo = newData.citizenNo;
                try
                {

                    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", newData.citizenNo);
                    var citizens = _citizen.Find(filter).ToList();
                    ViewBag.UserName = citizens[0].Name + " " + citizens[0].SureName;
                    // Convert MongoDB documents to dynamic objects
                    var dynamicCitizen = new JArray();
                    foreach (var ct in citizens)
                    {
                        dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                    }


                    //ViewBag.Citizens = dynamicCitizen;
                    // tbPeople
                    data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<mdEducation> edLs = new List<mdEducation>();
                    if (data.educationInfos == null || data.educationInfos.Count == 0)
                    {

                        edLs.Add(new mdEducation { });
                        data.educationInfos = edLs;
                    }
                    else
                    {
                        edLs = data.educationInfos;

                    }

                    edLs.Add(new mdEducation { citizenNo = newData.citizenNo, eduLevel = newData.eduLevel, eduInstitution = newData.eduInstitution });
                    data.educationInfos = edLs;
                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
                    return RedirectToAction("Index", _msg);
                    //return View("Index", data);
                    // return View("Index");
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View();
        }
        public ActionResult EditEdu(tbPeople data)
        {
            try
            {
                // var obj = new mdCitizen();

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
                    if (citizens.Count > 0)
                    {
                        //// Convert MongoDB documents to dynamic objects
                        //var dynamicCitizen = new JArray();
                        //foreach (var ct in citizens)
                        //{
                        //	dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                        //}


                        ////ViewBag.Citizens = dynamicCitizen;
                        //tbPeople existData = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                        //List<tbPeople> lsFam = new List<tbPeople>();
                        //try
                        //{

                        //	foreach (var fa in existData.family)
                        //	{

                        //		if (fa.citizenNo != data.citizenNo) lsFam.Add(fa);

                        //	}



                        //	SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        //	clsutil _uti = new clsutil();

                        //	data.updateDate = _uti.getSysDate();
                        //	db.UpdateData(data);

                        //}
                        //catch (Exception ex) { }
                        data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        //      data.ParentId = existData.citizenNo;
                        //       lsFam.Add(data);
                        //     existData.family = lsFam;
                        SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        clsutil _uti = new clsutil();
                        data.createDate = _uti.getSysDate();
                        data.updateDate = _uti.getSysDate();
                        //List<mdEducation> lsData = new List<mdEducation>();
                        //lsData.Add(data);
                        //citizens[0].educationInfos = lsData;


                        // db.UpdateData(citizens[0]);
                        db.SaveEduData(data);
                    }
                    else
                    {
                        // tbPeople editData = JsonSerializer.Deserialize<tbPeople>()
                        //data.id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        ////      data.ParentId = existData.citizenNo;
                        ////       lsFam.Add(data);
                        ////     existData.family = lsFam;
                        //SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        //clsutil _uti = new clsutil();
                        //data.createDate = _uti.getSysDate();
                        //data.updateDate = _uti.getSysDate();
                        //db.SaveEduData(data);
                    }



                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
                    return RedirectToAction("Index", _msg);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }

                return View("/SmartCitizen", data);
            }
            catch (Exception ex)
            {
                var xx = ex;
                return RedirectToAction("Index");
            }
            ViewBag.Message = "ขอบคุณสำหรับการอัพเดทข้อมูล";
            return RedirectToAction("Index");// View("Index",data);
        }

        public ActionResult AddRelot(mdRelocation newData)
        {
            if (newData.citizenNo != null)
            {
                tbPeople data = new tbPeople();
                //    data.citizenNo = newData.citizenNo;
                try
                {

                    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", newData.citizenNo);
                    var citizens = _citizen.Find(filter).ToList();
                    ViewBag.UserName = citizens[0].Name + " " + citizens[0].SureName;
                    // Convert MongoDB documents to dynamic objects
                    var dynamicCitizen = new JArray();
                    foreach (var ct in citizens)
                    {
                        dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                    }


                    //ViewBag.Citizens = dynamicCitizen;
                    // tbPeople
                    data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<mdRelocation> edLs = new List<mdRelocation>();
                    if (data.relocationInfos == null || data.relocationInfos.Count == 0)
                    {

                        edLs.Add(new mdRelocation { });
                        data.relocationInfos = edLs;
                    }
                    else
                    {
                        edLs = data.relocationInfos;

                    }

                    edLs.Add(new mdRelocation { citizenNo = newData.citizenNo, reLoProvice = newData.reLoProvice, reLoSinceYear = newData.reLoSinceYear, reLoToYear = newData.reLoToYear });
                    data.relocationInfos = edLs;
                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
                    return RedirectToAction("Index", _msg);
                   // return View("Index", data);
                    // return View("Index");
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View();
        }
        public ActionResult EditRelot(tbPeople data)
        {
            var _msg = new Message();
            try
            {
                // var obj = new mdCitizen();

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
                    if (citizens.Count > 0)
                    {

                        data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                        SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        clsutil _uti = new clsutil();
                        data.createDate = _uti.getSysDate();
                        data.updateDate = _uti.getSysDate();



                        // db.UpdateData(citizens[0]);
                        db.SaveRelotData(data);
                    }
                    else
                    {
                        // tbPeople editData = JsonSerializer.Deserialize<tbPeople>()
                        //data.id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        ////      data.ParentId = existData.citizenNo;
                        ////       lsFam.Add(data);
                        ////     existData.family = lsFam;
                        //SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        //clsutil _uti = new clsutil();
                        //data.createDate = _uti.getSysDate();
                        //data.updateDate = _uti.getSysDate();
                        //db.SaveEduData(data);
                    }



                  
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
                    return RedirectToAction("Index", _msg);
                   // return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }

                return View("/SmartCitizen", data);
            }
            catch (Exception ex)
            {
                var xx = ex;
                return RedirectToAction("Index");
            }
             
            _msg.title = "Info";
            _msg.text = "Save Success";
            _msg.icon = "success";

            //  return RedirectToAction("Index",new Message{ icon ="Success", text="Success",title="Info"});
            return RedirectToAction("Index", _msg);
         //   return RedirectToAction("Index");// View("Index",data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGraduation(int id, tbPeople collection)
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

        // GET: SmartCityzenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SmartCityzenController/Delete/5
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
