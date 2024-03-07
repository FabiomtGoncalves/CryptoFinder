using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer.Symbols;

namespace CryptoExchange.Controllers
{
    public class ExchangesController : Controller
    {

        private string baseUrl = "https://api.coingecko.com/api/v3/";
        private WebClient web = new WebClient();

        // GET: Exchanges
        public ActionResult Index()
        {
            //var result = JsonConvert.DeserializeObject(responseString);


            try
            {
                var responseString = web.DownloadString(baseUrl + "exchanges");
                var objects = JArray.Parse(responseString);
                ViewBag.obj = objects;
            }
            catch (Exception e)
            {
                ViewData["exchanges"] = "Too Many API Requests" + e;
            }


            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(string searchCrypto)
        {
            if (searchCrypto.Trim() == "")
            {
                ViewBag.error = "!erro!";
            }
            else
            {
                try
                {
                    var responseString = web.DownloadString(baseUrl + "search?query=" + searchCrypto);
                    var objects = JObject.Parse(responseString);
                    var arrayObj = JArray.Parse(objects["coins"].ToString());
                    System.Diagnostics.Debug.WriteLine("OBJ: " + arrayObj);
                    ViewBag.cryptos = arrayObj;
                }
                catch (Exception e)
                {
                    ViewData["exchanges"] = "Too Many API Requests" + e;
                }
            }

            return View();

        }

    }
}