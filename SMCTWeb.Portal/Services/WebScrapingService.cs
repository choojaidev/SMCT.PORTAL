using SMCTPortal;
using SMCTPortal.Model;
using SMCTPortal.Services;
   using HtmlAgilityPack;
    using System.Collections.Generic;
    using System.Linq;

namespace SMCTPortal.Services
{
 
    public class WebScrapingService
    {
        public List<ScrapedData> ScrapeWebsite(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var scrapedData = doc.DocumentNode
                .Descendants("a")
                .Select(a => new ScrapedData
                {
                    Title = a.InnerText,
                    Url = a.GetAttributeValue("href", "")
                })
                .Where(a => !string.IsNullOrEmpty(a.Title) && !string.IsNullOrEmpty(a.Url))
                .ToList();

            return scrapedData;
        }
    }

}
