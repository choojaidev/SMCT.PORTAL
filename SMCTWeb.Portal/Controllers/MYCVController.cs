﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Model;
using SMCTPortal.Repository;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Text.Json;
using MongoDB.Bson;
using System.Linq;
using SMCTPortal.Model.SMCV;
using System.Security.Cryptography;
using Amazon.Runtime.EventStreams.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace SMCTPortal.Controllers
{

    public class MYCVController : Controller
    {
        private readonly MongoRepository _mongoRepository;
        private readonly SMCTDbContext _context;
        private readonly MongoDBContext _mongoDBContext;
        private readonly IMongoClient _mongoClient;
        private readonly IMongoCollection<tbPeople> _citizen;
        public class Message
        {
            public string title { get; set; }
            public string text { get; set; }
            public string icon { get; set; }

        }

        clsutil _uti = new clsutil();
        private readonly IWebHostEnvironment _environment;

        public MYCVController(SMCTDbContext context, MongoRepository mongoRepository, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
            _mongoRepository = mongoRepository;

            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SMZT");
            _citizen = database.GetCollection<tbPeople>("Citizens");

        }
        // GET: MYCVController
        public ActionResult Index( Message msg )
        {
            if (msg.text != "") ViewBag.Msg = msg;
            tbPeople data = new tbPeople();
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

                    if (existData.resumeInfos == null)
                    {

                        var _resume = new Resume();
                        _resume.citizenNo = existData.citizenNo;
                        _resume.Name = existData.Name;
                        _resume.SureName = existData.SureName;
                        _resume.educationInfos = existData.educationInfos;
                        try {
                            _resume.Age = (int.Parse ( _uti.getSysDate(false).Substring (0,4) )- int.Parse ( _resume.DateOfBirth.Substring(0,4))).ToString ();
                        }catch(Exception ex) { }
                        try {
                            _resume.Provice = "";
                        }catch(Exception ex) { }

                        existData.resumeInfos = _resume;

                    }
                    if (existData.resumeInfos.JobHistoryInfos == null)
                    {
                        var _jobHist = new List<mdJobHistory>();
                        _jobHist.Add(new mdJobHistory { citizenNo = existData.citizenNo });
                        existData.resumeInfos.JobHistoryInfos = _jobHist;
                    }
                    if (existData.resumeInfos.educationInfos == null)
                    {
                        existData.resumeInfos.educationInfos = existData.educationInfos;
                    }
                    if (existData.resumeInfos.educationInfos == null)
                    {
                        var _eduHist = new List<mdEducation>();
                        _eduHist.Add(new mdEducation { citizenNo = existData.citizenNo });
                        existData.resumeInfos.educationInfos = _eduHist;
                    }


                    if (existData.resumeInfos.SkillInfos == null)
                    {
                        var _skillHist = new List<mdSkill>();
                        _skillHist.Add(new mdSkill {  id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) , SkillName = "", SkillValue = ""   });
                        existData.resumeInfos.SkillInfos  = _skillHist;
                    }


                    if (existData.resumeInfos.SocialMediaInfos == null)
                    {
                        var _lsSocial = new List<mdSocialMedia>();
                        _lsSocial.Add(new mdSocialMedia  { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), SocialName  = "", SocialURL = "" });
                        existData.resumeInfos.SocialMediaInfos= _lsSocial;
                    }





                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    // DataAccess.database db = new DataAccess.database(_mongoClient);
                    // db.UpdateFamilyData(existData);
                    // return RedirectToAction(nameof(Index), existData);
                    return View("IndexDsp1",existData.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
                return View( data.resumeInfos);
                // return RedirectToAction(nameof(Index), data);
            }
            catch
            {
                return View("IndexDsp1");
            }
          
        }

        // GET: MYCVController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MYCVController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MYCVController/Create
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

        // GET: MYCVController/Edit/5
        public ActionResult Edit(tbPeople data,Message msg)
        {
            if (msg.text != "") ViewBag.Msg = msg;
            //if (data.citizenNo != null) { return View("IndexEdit", data.resumeInfos); }
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

                    if (existData.resumeInfos == null)
                    {
                    
                        var _resume = new Resume();
                        _resume.citizenNo = existData.citizenNo;
                        _resume.Name = existData.Name;
                        _resume.SureName = existData.SureName;
                        _resume.educationInfos = existData.educationInfos;
            
                       
                        existData.resumeInfos = _resume;

                    }
                    if (existData.resumeInfos.JobHistoryInfos == null) {
                        var _jobHist = new List<mdJobHistory>();
                        _jobHist.Add(new mdJobHistory { citizenNo = existData.citizenNo });
                        existData.resumeInfos.JobHistoryInfos = _jobHist;
                    }
                    if (existData.resumeInfos.educationInfos == null)
                    {
                        existData.resumeInfos.educationInfos = existData.educationInfos;
                    }
                    if (existData.resumeInfos.educationInfos == null) {
                        var _eduHist = new List<mdEducation>();
                        _eduHist.Add(new mdEducation { citizenNo = existData.citizenNo });
                        existData.resumeInfos.educationInfos = _eduHist;
                    }


                    if (existData.resumeInfos.SkillInfos == null)
                    {
                        var _skillHist = new List<mdSkill>();
                        _skillHist.Add(new mdSkill { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), SkillName = "", SkillValue = "" });
                        existData.resumeInfos.SkillInfos = _skillHist;
                    }

                    if (existData.resumeInfos.SocialMediaInfos == null)
                    {
                        var _lsSocial = new List<mdSocialMedia>();
                        _lsSocial.Add(new mdSocialMedia { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), SocialName = "", SocialURL = "" });
                        existData.resumeInfos.SocialMediaInfos = _lsSocial;
                    }


                    //if (data.citizenNo != null) {
                    //    existData.resumeInfos = data.resumeInfos;
                    //}


                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";

                    // DataAccess.database db = new DataAccess.database(_mongoClient);
                    // db.UpdateFamilyData(existData);
                    // return RedirectToAction(nameof(Index), existData);
                    return View("IndexEdit", existData.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
                return View("IndexEdit", data.resumeInfos);
               // return RedirectToAction(nameof(Index), data);
            }
            catch
            {
                return View();
            }
            return View("IndexEdit",data);
           // return View("IndexEdit");
        }
        public ActionResult EditCV(Resume data, Message msg)
        {
            if (msg.text != "") ViewBag.Msg = msg;
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
                       var _resume = new Resume();
                    if (existData.resumeInfos == null)
                    {
                        
                    _resume._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                        _resume.createDate = _uti.getSysDate(true);
                    }
                    else {
                        _resume = existData.resumeInfos;
                    }
                    if (_resume._id == null) { _resume._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()); }
                    _resume.citizenNo = existData.citizenNo;
                        _resume.updateDate = _uti.getSysDate(true);
                        _resume.Name = data.Name;
                        _resume.SureName = data.SureName;
                        _resume.DateOfBirth = data.DateOfBirth;
                        _resume.PhoneNo = data.PhoneNo;
                        _resume.Address = data.Address;
                        _resume.Email = data.Email;
                        _resume.Description = data.Description; 
                    _resume.FullDescription = data.FullDescription;
                    _resume.HashTag = data.HashTag;
                    if(_resume.educationInfos == null) _resume.educationInfos = existData.educationInfos;
                    existData.resumeInfos = _resume;

                    
                    //  data.ParentId = existData.citizenNo;

                    data.updateDate = _uti.getSysDate(true);
                    //   lsFam.Add(data);
                  //  existData.family = lsFam;
                  


                    DataAccess.database db = new DataAccess.database(_mongoClient);
                    db.SaveCVData(existData);
                    // return RedirectToAction(nameof(Index), existData);
                    return View("IndexEdit", existData.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }

              //  return RedirectToAction(nameof(Index), data);
            }
            catch
            {
                return View();
            }
            return View("IndexEdit", data);
            // return View("IndexEdit");
        }


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

                    data.resumeInfos.educationInfos = data.educationInfos;
                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    ViewBag.Msg = _msg;
                    
                   // return RedirectToAction("Edit",data);
                    return View("IndexEdit", data.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View();
        }
        public ActionResult EditEdu(Resume data)
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
                         
                       // data._id = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                         
                        SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
                        clsutil _uti = new clsutil();
                        data.createDate = _uti.getSysDate();
                        data.updateDate = _uti.getSysDate();
                        var saveData = new tbPeople();
                        saveData.citizenNo = data.citizenNo;
                        saveData.educationInfos = data.educationInfos;
                        db.SaveEduData(saveData);
                    }
                    else
                    {
                        
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

      

        public ActionResult AddWorkHist(mdJobHistory newData)
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



                    data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<mdJobHistory> jbLs = new List<mdJobHistory>();
                    if (data.resumeInfos.JobHistoryInfos == null || data.resumeInfos.JobHistoryInfos.Count == 0)
                    {

                        jbLs.Add(new mdJobHistory { });
                        data.resumeInfos.JobHistoryInfos = jbLs;
                    }
                    else
                    {
                        jbLs = data.resumeInfos.JobHistoryInfos;

                    }

                    jbLs.Add(new mdJobHistory { citizenNo = newData.citizenNo,
                        ComName = newData.ComName,
Position = newData.Position, 
                        Responsibility = newData.Responsibility,
                     SinceYear = newData.SinceYear,
                     ToYear = newData.ToYear,
                    id = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                });
                    data.resumeInfos.JobHistoryInfos = jbLs;

                   
                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    ViewBag.Msg = _msg;

                    // return RedirectToAction("Edit",data);
                    return View("IndexEdit", data.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View();
        }
        public ActionResult AddExportHist(mdExportHist data)
        {
            //if (newData.citizenNo != null)
            //{
              SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
          //  db.SaveCVJobHistData(data);
             try
            {
            //    var uid = User.Claims;
            //    var xid = (from cc in User.Claims
            //               where cc.Type.ToString().IndexOf("sub") > -1
            //               select cc).ToList();
            //    try
            //    {//

            //     //   clsutil _uti = new clsutil();
            //        data.createDate = _uti.getSysDate();
            //        data.updateDate = _uti.getSysDate();
            //        var saveData = new tbPeople();
            //        saveData.citizenNo = data.citizenNo;
            //        saveData.educationInfos = data.educationInfos;

            //        data.updateDate = _uti.getSysDate(true);
                  
            //        db.SaveCVJobHistData(data);

            //        var _msg = new Message();
            //        _msg.title = "Info";
            //        _msg.text = "Save Success";
            //        _msg.icon = "success";
            //        // return RedirectToAction(nameof(Index), existData);
            //        return RedirectToAction(nameof(Index), data);
            //    }
            //    catch (Exception ex)
            //    {
            //        var xx = ex;
            //    }

                return RedirectToAction(nameof(Index), data);
            }
            catch
            {
                return View();
            }

            return View();
        }
        
   public ActionResult EditWorkHist(Resume data)
        {
            //if (newData.citizenNo != null)
            //{
              SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
          //  db.SaveCVJobHistData(data);
            try
            {
                var uid = User.Claims;
                var xid = (from cc in User.Claims
                           where cc.Type.ToString().IndexOf("sub") > -1
                           select cc).ToList();
                try
                {//

                 //   clsutil _uti = new clsutil();
                    data.createDate = _uti.getSysDate();
                    data.updateDate = _uti.getSysDate();
                    var saveData = new tbPeople();
                    saveData.citizenNo = data.citizenNo;
                    saveData.educationInfos = data.educationInfos;

                    data.updateDate = _uti.getSysDate(true);
                  
                    db.SaveCVJobHistData(data);

                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    // return RedirectToAction(nameof(Index), existData);
                    return RedirectToAction(nameof(Index), data);
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

            return View();
        }


        public ActionResult EditSkill(Resume data)
        {
            //if (newData.citizenNo != null)
            //{
            SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
            //  db.SaveCVJobHistData(data);
            try
            {
                var uid = User.Claims;
                var xid = (from cc in User.Claims
                           where cc.Type.ToString().IndexOf("sub") > -1
                           select cc).ToList();
                try
                {//

                    //   clsutil _uti = new clsutil();
                    data.createDate = _uti.getSysDate();
                    data.updateDate = _uti.getSysDate();
                    var saveData = new tbPeople();
                    saveData.citizenNo = data.citizenNo;
                 //   saveData.resumeInfos.SkillInfos = data.SkillInfos ;

                    data.updateDate = _uti.getSysDate(true);

                    db.SaveSkillData(data);

                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    // return RedirectToAction(nameof(Index), existData);
                    return RedirectToAction(nameof(Index), data);
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

            return View();
        }

        public ActionResult AddSkill(mdSkill newData) {
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



                    data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<mdSkill> skillLs = new List<mdSkill>();
                    if (data.resumeInfos.SkillInfos  == null || data.resumeInfos.SkillInfos.Count == 0)
                    {

                        skillLs.Add(new mdSkill { });
                        data.resumeInfos.SkillInfos = skillLs;
                    }
                    else
                    {
                        skillLs = data.resumeInfos.SkillInfos;

                    }

                    skillLs.Add(new mdSkill
                    {
                        citizenNo = newData.citizenNo,
                        id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                        SkillName = newData.SkillName,
                        SkillValue = newData.SkillValue
                    });
                    data.resumeInfos.SkillInfos = skillLs;


                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    ViewBag.Msg = _msg;

                    // return RedirectToAction("Edit",data);
                    return View("IndexEdit", data.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View(newData);
        }


        public ActionResult EditSocial(Resume data)
        {
            //if (newData.citizenNo != null)
            //{
            SMCTPortal.DataAccess.database db = new SMCTPortal.DataAccess.database(_mongoClient);
            //  db.SaveCVJobHistData(data);
            try
            {
                var uid = User.Claims;
                var xid = (from cc in User.Claims
                           where cc.Type.ToString().IndexOf("sub") > -1
                           select cc).ToList();
                try
                {//

                    //   clsutil _uti = new clsutil();
             
                    var saveData = new tbPeople();
                    saveData.citizenNo = data.citizenNo;
                    //   saveData.resumeInfos.SkillInfos = data.SkillInfos ;

                  

                    db.SaveSocialData(data);

                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    // return RedirectToAction(nameof(Index), existData);
                    return RedirectToAction(nameof(Index), data);
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

            return View();
        }

     
            //[HttpPost]
            //public ActionResult updateImgProfile(HttpPostedFileBase file)
             [HttpPost]
        public async Task<IActionResult> updateImgProfile(IFormFile file)
        {
            tbPeople existData = new tbPeople();
            // if (msg.text != "") ViewBag.Msg = msg;
            //if (data.citizenNo != null) { return View("IndexEdit", data.resumeInfos); }
           
           
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
                    //   var filter = Builders<tbPeople>.Filter.Eq("_id", data._id);
                    var filter = Builders<tbPeople>.Filter.Eq("citizenNo", xid[0].Subject.Name.ToString());

                    var citizens = _citizen.Find(filter).ToList();

                    // store avatar
                    string newFileName = "";
                    try
                    {
                        if (file != null && file.Length > 0)
                        {

                            var fileName = Path.GetFileName(file.FileName);
                            var extention = Path.GetExtension(file.FileName).Split('.')[1];
                            newFileName = _uti.getSysDate(true) + xid[0].Subject.Name.ToString() + "." + extention; //Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                            var path = Path.Combine(_environment.WebRootPath, "images\\CV\\avatar\\", newFileName);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            ViewBag.Message = "File uploaded successfully.";
                        }
                        else
                        {
                            ViewBag.Message = "No file selected.";
                        }

                    }
                    catch
                    {
                        ViewBag.Message = "No file selected.";

                    }


                    // Convert MongoDB documents to dynamic objects
                    var dynamicCitizen = new JArray();
                    foreach (var ct in citizens)
                    {
                        dynamicCitizen.Add(JObject.Parse(ct.ToJson()));
                    }


                    //ViewBag.Citizens = dynamicCitizen;
                      existData = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<tbPeople> lsFam = new List<tbPeople>();

                    if (existData.resumeInfos == null)
                    {

                        var _resume = new Resume();
                        _resume.citizenNo = existData.citizenNo;
                        _resume.Name = existData.Name;
                        _resume.SureName = existData.SureName;
                        _resume.educationInfos = existData.educationInfos;

                   
                        existData.resumeInfos = _resume;

                    }
                    if (existData.resumeInfos.JobHistoryInfos == null)
                    {
                        var _jobHist = new List<mdJobHistory>();
                        _jobHist.Add(new mdJobHistory { citizenNo = existData.citizenNo });
                        existData.resumeInfos.JobHistoryInfos = _jobHist;
                    }
                    if (existData.resumeInfos.educationInfos == null)
                    {
                        existData.resumeInfos.educationInfos = existData.educationInfos;
                    }
                    if (existData.resumeInfos.educationInfos == null)
                    {
                        var _eduHist = new List<mdEducation>();
                        _eduHist.Add(new mdEducation { citizenNo = existData.citizenNo });
                        existData.resumeInfos.educationInfos = _eduHist;
                    }


                    if (existData.resumeInfos.SkillInfos == null)
                    {
                        var _skillHist = new List<mdSkill>();
                        _skillHist.Add(new mdSkill { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), SkillName = "", SkillValue = "" });
                        existData.resumeInfos.SkillInfos = _skillHist;
                    }

                    if (existData.resumeInfos.SocialMediaInfos == null)
                    {
                        var _lsSocial = new List<mdSocialMedia>();
                        _lsSocial.Add(new mdSocialMedia { id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()), SocialName = "", SocialURL = "" });
                        existData.resumeInfos.SocialMediaInfos = _lsSocial;
                    }


                    //if (data.citizenNo != null) {
                    //    existData.resumeInfos = data.resumeInfos;
                    //}
                   
                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    ViewBag.Msg = _msg;
                    existData.resumeInfos.Avatar = newFileName;
                    DataAccess.database db = new DataAccess.database(_mongoClient);
                    db.UpdateAvatarData(existData.resumeInfos);
                    // return RedirectToAction(nameof(Index), existData);
                    return RedirectToAction(nameof(Edit), existData.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
                return RedirectToAction(nameof(Edit), existData.resumeInfos);
                // return RedirectToAction(nameof(Index), data);
            }
            catch(Exception ex)
            {
                var _msg = new Message();
                _msg.title = "Error";
                _msg.text = "Error " + ex.Message .ToString ();
                _msg.icon = "Error";
                ViewBag.Msg = _msg;
                return RedirectToAction(nameof(Edit), existData.resumeInfos);
            }
            return RedirectToAction(nameof(Edit), existData.resumeInfos);
        }
       public ActionResult AddSocial(mdSocialMedia newData)
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



                    data = JsonSerializer.Deserialize<tbPeople>(dynamicCitizen.Root[0].ToString());
                    List<mdSocialMedia> socials = new List<mdSocialMedia>();
                    if (data.resumeInfos.SocialMediaInfos == null || data.resumeInfos.SocialMediaInfos.Count == 0)
                    {

                        socials.Add(new mdSocialMedia { });
                        data.resumeInfos.SocialMediaInfos= socials;
                    }
                    else
                    {
                        socials = data.resumeInfos.SocialMediaInfos;

                    }

                    socials.Add(new mdSocialMedia
                    {
                        citizenNo = newData.citizenNo,
                        id = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),
                        SocialName  = newData.SocialName,
                        SocialURL  = newData.SocialURL 
                    });
                    data.resumeInfos.SocialMediaInfos= socials;


                    var _msg = new Message();
                    _msg.title = "Info";
                    _msg.text = "Save Success";
                    _msg.icon = "success";
                    ViewBag.Msg = _msg;

                    // return RedirectToAction("Edit",data);
                    return View("IndexEdit", data.resumeInfos);
                }
                catch (Exception ex)
                {
                    var xx = ex;
                }
            }

            return View(newData);
        }


        // POST: MYCVController/Edit/5
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

        // GET: MYCVController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MYCVController/Delete/5
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
