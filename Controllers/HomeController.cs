using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CryptoExchange.Controllers
{
    public class HomeController : Controller
    {

        private string baseUrl = "https://api.coingecko.com/api/v3/coins/";
        private WebClient web = new WebClient();
        DateTime date = DateTime.Today;

        private JObject readAPI(string url)
        {
            try
            {
                var responseString = web.DownloadString(baseUrl + url + date.ToString("dd-MM-yyyy"));
                return JObject.Parse(responseString);
            } catch (Exception e)
            {
                return null;
            }
                
        }

        public ActionResult Index()
        {
            ViewBag.btc = readAPI("bitcoin/history?date=");
            ViewBag.eth = readAPI("ethereum/history?date=");
            ViewBag.date = date.ToString("MMMM d, yyyy");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Testing Crypto app.";

            return View();
        }


        public ActionResult Testing()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Testing(string cryptoName, string symbolName)
        {
            if (cryptoName.Trim() == "" || symbolName.Trim() == "")
            {
                ViewBag.Name = string.Format("!erro!");
            }
            else
            {
                ViewBag.Name = string.Format("Crypto: {0} ${1} is available!", cryptoName, symbolName);
            }
            return View();
        }


    }
}