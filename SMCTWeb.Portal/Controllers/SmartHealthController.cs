using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SMCTPortal.Model;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Repository;
using System;
using SMCTPortal.Model.SMPeople;
using SMCTPortal.Model;
using SMCTPortal.Repository;
using Newtonsoft.Json.Linq;
using SMCTPortal.Model.SMCV;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Text.Json;

namespace SMCTPortal.Controllers
{
    public class SmartHealthController : Controller
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
		public SmartHealthController(SMCTDbContext context, MongoRepository mongoRepository, IWebHostEnvironment environment)
        {
			_environment = environment;
			_context = context;
			_mongoRepository = mongoRepository;

			var client = new MongoClient("mongodb://localhost:27017");
			var database = client.GetDatabase("SMZT");
			_citizen = database.GetCollection<tbPeople>("Citizens");
		}
        // GET: SmartHealthController
        public ActionResult Index(Message msg)
        {
			// feature
			// basic info about heath
			// health history
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
						try
						{
							_resume.Age = (int.Parse(_uti.getSysDate(false).Substring(0, 4)) - int.Parse(_resume.DateOfBirth.Substring(0, 4))).ToString();
						}
						catch (Exception ex) { }
						try
						{
							_resume.Provice = "";
						}
						catch (Exception ex) { }

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





					var _msg = new Message();
					_msg.title = "Info";
					_msg.text = "Save Success";
					_msg.icon = "success";

					// DataAccess.database db = new DataAccess.database(_mongoClient);
					// db.UpdateFamilyData(existData);
					// return RedirectToAction(nameof(Index), existData);
					return View(existData.resumeInfos);
				}
				catch (Exception ex)
				{
					var xx = ex;
				}
				return View(data.resumeInfos);
				// return RedirectToAction(nameof(Index), data);
			}
			catch
			{
				return View();
			}
			 
        }

        // GET: SmartHealthController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SmartHealthController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SmartHealthController/Create
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

        // GET: SmartHealthController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SmartHealthController/Edit/5
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

        // GET: SmartHealthController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SmartHealthController/Delete/5
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
