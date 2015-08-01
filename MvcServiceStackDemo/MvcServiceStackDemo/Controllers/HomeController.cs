using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcServiceStackDemo.Models;

namespace MvcServiceStackDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModel contactModel)
        {
            if (SendMail(contactModel))
            {
                ViewBag.MailSent = true;
            }
            return View();
        }

        public bool SendMail(ContactModel mail)
        {
            return true;
        }
    }
}