using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteBackEnd.Models;

namespace ESWWebsite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            List<jpUSER> lstjpUSER = jpUSERManager.GetjpUSER("", null);

            return View();
        }
    }
}
