using Microsoft.AspNetCore.Mvc;
using SMCTPortal.Model;
using SMCTPortal.Services;

namespace SMCTPortal.Controllers
{
    public class NewsController : Controller
    {
        private readonly WebScrapingService _webScrapingService;

        //public HomeController(WebScrapingService webScrapingService)
        //{
        //    _webScrapingService = webScrapingService;
        //}
        public NewsController(WebScrapingService webScrapingService)
        {
            _webScrapingService = webScrapingService;
        }
        public IActionResult Index()
        {
            // Replace the URL with the target website you want to scrape
            string targetUrl = "https://www.thairath.co.th/tags/%E0%B8%AA%E0%B8%B8%E0%B8%A3%E0%B8%B2%E0%B8%A9%E0%B8%8E%E0%B8%A3%E0%B9%8C%E0%B8%98%E0%B8%B2%E0%B8%99%E0%B8%B5";
            var scrapedData = _webScrapingService.ScrapeWebsite(targetUrl);

            return View(scrapedData);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
