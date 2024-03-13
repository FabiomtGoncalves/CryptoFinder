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

        private JArray readAPI(string url)
        {
            var responseString = web.DownloadString(baseUrl + url);
            return JArray.Parse(responseString);
        }

        // GET: Exchanges
        public ActionResult Index()
        {

            try
            {
                //var responseString = web.DownloadString(baseUrl + "exchanges");
                //var objects = JArray.Parse(responseString);
                string url = "exchanges";
                ViewBag.obj = readAPI(url);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                ViewBag.obj = null;
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
                    ViewBag.cryptos = arrayObj;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                    ViewBag.cryptos = null;
                }
            }

            return View();

        }

    }
}