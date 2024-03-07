using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CryptoExchange.Controllers
{
    public class HomeController : Controller
    {

        private static HttpClient sharedClient = new HttpClient()
        {
            BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),

        };

       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

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