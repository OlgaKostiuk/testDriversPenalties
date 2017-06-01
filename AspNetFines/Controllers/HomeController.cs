using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AspNetFines.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string password = Crypto.HashPassword("123");
            Guid guid = Guid.NewGuid();
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
    }
}