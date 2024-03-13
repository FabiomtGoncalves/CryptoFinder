using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace CryptoExchange.Controllers
{

    [Authorize]
    public class CryptoFinderAPIController : Controller
    {

        private string baseUrl = "https://localhost:7290/api/Crypto/";
        private WebClient web = new WebClient();

        private JArray readAPI(string url)
        {
            try
            {
                var responseString = web.DownloadString(baseUrl + url);
                return JArray.Parse(responseString);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                return null;
            }

        }

        // GET: CryptoFinderAPI
        public ActionResult Index()
        {
            ViewBag.ping = web.DownloadString(baseUrl + "ping");
            return View();
        }
    }
}