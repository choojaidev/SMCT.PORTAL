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

namespace SMCTPortal.Controllers
{
    public class HomeController : Controller
    {
        private const string TMD_7DAYS_API_URL = "https://tmd.go.th/api/WeatherForecast7Day/weather-forecast-7day-by-province?Sorting=weatherForecast7Day.recordTime asc&FilterText={0}&MaxResultCount=7&culture=th-TH";
        private const string TMD_3Hour_API_URL = "https://tmd.go.th/api/Weather3Hour/weather-3hour-province?FilterText={0}&culture=th-TH";
        private const string PM_API_URL = "http://api.airvisual.com/v2/city?city=Surat Thani&state=Surat Thani&country=Thailand&key={0}";
        private const string API_KEY = "03ba6fd9-37b2-48f7-88ac-ec4d7100ea16";
        private const string PROVINCE_NAME = "สุราษฎร์ธานี";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IDiscoveryCache _discoveryCache;

        public HomeController(IHttpClientFactory httpClientFactory, IDiscoveryCache discoveryCache)
        {
            _httpClientFactory = httpClientFactory;
            _discoveryCache = discoveryCache;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            #region Weather Forecast
            var forecast7DaysResponse = await client.GetStringAsync(string.Format(TMD_7DAYS_API_URL, PROVINCE_NAME));
            var forecast7DaysModel = JsonConvert.DeserializeObject<List<WeatherForecast7DayResponse>>(forecast7DaysResponse);

            var forecast3HouseResponse = await client.GetStringAsync(string.Format(TMD_3Hour_API_URL, PROVINCE_NAME));
            var forecast3HouseModel = JsonConvert.DeserializeObject<Weather3HourResponse>(forecast3HouseResponse);
            #endregion
            var pmResponse = await client.GetStringAsync(string.Format(PM_API_URL, API_KEY));
            var pmModel = JsonConvert.DeserializeObject<PMResponse>(pmResponse);
            #region PM

            #endregion
        //return RedirectToAction("Index","Portal");// go to portal
            return View(new ForecastResponse { 
                WeatherForecast7DayResponse = forecast7DaysModel,
                Weather3HourResponse = forecast3HouseModel,
                PMResponse = pmModel
            });
        }

        public IActionResult Secure() { 
            return RedirectToAction("Index");
        }

        public IActionResult Logout() => SignOut("oidc", "Cookies");

        public async Task<IActionResult> CallApi()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);

            var response = await client.GetStringAsync(Constants.SampleApi + "identity");
            ViewBag.Json = response.PrettyPrintJson();

            return View();
        }

        public async Task<IActionResult> RenewTokens()
        {
            var disco = await _discoveryCache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var rt = await HttpContext.GetTokenAsync("refresh_token");
            var tokenClient = _httpClientFactory.CreateClient();

            var tokenResult = await tokenClient.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "mvc.code",
                ClientSecret = "secret",
                RefreshToken = rt
            });

            if (!tokenResult.IsError)
            {
                var oldIdToken = await HttpContext.GetTokenAsync("id_token");
                var newAccessToken = tokenResult.AccessToken;
                var newRefreshToken = tokenResult.RefreshToken;
                var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenResult.ExpiresIn);

                var info = await HttpContext.AuthenticateAsync("Cookies");

                info.Properties.UpdateTokenValue("refresh_token", newRefreshToken);
                info.Properties.UpdateTokenValue("access_token", newAccessToken);
                info.Properties.UpdateTokenValue("expires_at", expiresAt.ToString("o", CultureInfo.InvariantCulture));

                await HttpContext.SignInAsync("Cookies", info.Principal, info.Properties);
                return Redirect("~/Home/Secure");
            }

            ViewData["Error"] = tokenResult.Error;
            return View("Error");
        }
    }
}