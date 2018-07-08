using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ESWWebsite.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Error()
        {
            return View("~/Views/Shared/Error404.cshtml");
        }
        public ActionResult Index()
        {
            return View("Home");
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult _EduTabView()
        {
            return PartialView();
        }
        public ActionResult _ExpTabView()
        {
            return PartialView();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        public ActionResult JobCard()
        {
            return View();
        }
        public ActionResult TabView()
        {
            return View();
        }
        public ActionResult SubmitResume()
        {
            return View();
        }
        public ActionResult PostJobs()
        {
            return View();
        }
    }
}