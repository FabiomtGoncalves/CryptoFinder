using CryptoExchange.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.IO;
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

        private String readAPI(string url)
        {
            try
            {
                return web.DownloadString(baseUrl + url);
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
            try
            {
                var responseString = web.DownloadString(baseUrl + "all");
                ViewBag.ping = readAPI("ping");
                ViewBag.all = JArray.Parse(responseString);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                ViewBag.ping = null;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string cryptoName, string symbolName, string desc)
        {

            try
            {
                var responseString = web.DownloadString(baseUrl + "all");
                ViewBag.ping = readAPI("ping");
                ViewBag.all = JArray.Parse(responseString);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                ViewBag.ping = null;
            }

            if (cryptoName.Trim() == "" || symbolName.Trim() == "" || desc.Trim() == "")
            {
                ViewBag.error = "!erro!";
            }
            else
            {
                try
                {

                    Crypto crypto = new Crypto()
                    {
                        Name = cryptoName,
                        Symbol = symbolName,
                        Desc = desc
                    };

                    var httpWebRequest = (HttpWebRequest)WebRequest.Create(baseUrl);
                    httpWebRequest.ContentType = "application/json";
                    httpWebRequest.Method = "POST";

                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = "{\"name\":\"" + cryptoName + "\"," + "\"symbol\":\"" + symbolName + "\"," +
                                      "\"desc\":\"" + desc +"\"}";

                        streamWriter.Write(json);
                    }

                    var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        var result = streamReader.ReadToEnd();
                    }

                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine("ERRO: " + e);
                }
            }

            return View();

        }

    }
}