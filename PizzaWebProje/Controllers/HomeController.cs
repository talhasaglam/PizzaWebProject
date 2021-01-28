using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebProje.Models;
using System.Globalization;

namespace PizzaWebProje.Controllers
{
    public class HomeController : Controller
    {
        PizzaSitesiDB db = new PizzaSitesiDB();

        public ActionResult Index()
        {
            var pizzaa = db.Pizza.Where(x => x.PizzaId > 0);
            return View(pizzaa);
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

        public ActionResult DilDegistir(string dil, string returnurl)
        {
            Session["dil"] = new CultureInfo(dil);
            return Redirect(returnurl);
        }

        public ActionResult SatinAl()
        {
            ViewBag.PizzaId = new SelectList(db.Pizza, "PizzaId", "PizzaId");
            ViewBag.UyeId = new SelectList(db.Uye, "UyeId", "UyeId");

            return View();
        }

        [HttpPost]
        public ActionResult SatinAl(Mesaj mesaj)
        {
            

            if (ModelState.IsValid)
            {

                

                db.Mesaj.Add(mesaj);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return View(mesaj);
        }

    }
}