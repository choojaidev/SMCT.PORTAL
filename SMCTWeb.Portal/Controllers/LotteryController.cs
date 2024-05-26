using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Azure;
using SMCTWebTemplate.Model;
using Newtonsoft.Json;
using SMCTPortal.Model.SMAdditional;

namespace SMCTPortal.Controllers
{
     
    public class LotteryController : Controller
    {
        // GET: LotteryController
        public async Task<ActionResult> Index()
        {
            Lotterys.contentData data =  await  getData();


            return View(data);
        }
        public   async Task<Lotterys.contentData> getData() {
            //try
            //{ }
            //catch (Exception ex) {
            //    return ex.Message;
            //}
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://www.glo.or.th/api/lottery/getLatestLottery");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
            //  Console.WriteLine(await response.Content.ReadAsStringAsync());
            string jsonResponse = await response.Content.ReadAsStringAsync();

        //    Lotterys data = new Lotterys();
            Lotterys.contentData data = JsonConvert.DeserializeObject<Lotterys.contentData>(jsonResponse);
      
            return data;
           
           
        }
        // GET: LotteryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LotteryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LotteryController/Create
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

        // GET: LotteryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LotteryController/Edit/5
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

        // GET: LotteryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LotteryController/Delete/5
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
