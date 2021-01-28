using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebProje.Models;

namespace PizzaWebProje.Controllers
{
    public class AdminController : Controller
    {

        PizzaSitesiDB db = new PizzaSitesiDB();
        // GET: Admin
        public ActionResult Index()
        {
           
            return View();
        }

        public ActionResult Mesaj()
        {
            var mesaj = db.Mesaj.ToList();
            return View(mesaj);

        }

        public ActionResult Uye()
        {
            var uye = db.Uye.ToList();
            return View(uye);

        }


    }
}