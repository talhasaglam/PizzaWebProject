using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebProje.Models;
using System.Web.Helpers;

namespace PizzaWebProje.Controllers
{
    public class UyeController : Controller
    {

        PizzaSitesiDB db = new PizzaSitesiDB();
        // GET: Uye
        public ActionResult Index(int id)
        {
            var uye = db.Uye.Where(x => x.UyeId == id).SingleOrDefault();
           if(Convert.ToInt32(Session["uyeid"]) != uye.UyeId)
            {
                return HttpNotFound();
            }

            return View(uye);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = db.Uye.Find(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Uye model)
        {
            if (ModelState.IsValid)
            {
                db.Uye.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Uye uye)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var login = db.Uye.FirstOrDefault(x => x.KullaniciAdi == uye.KullaniciAdi && x.Sifre == uye.Sifre);

            if(login!= null)

            {
                Session["uyeid"] = login.UyeId;
                Session["kullaniciadi"] = login.KullaniciAdi;
                Session["yetkiid"] = login.YetkiId;

                return RedirectToAction("Index", "Home");
            }

            else
            {
                ViewBag.Uyari = "Kullanici adi veya sifre hatali";
                return View();
            }

            
        }

        public ActionResult Logout()
        {
            Session["uyeid"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Uye uye)
        {
            
            if(ModelState.IsValid)
            {
                uye.YetkiId = 1;
                db.Uye.Add(uye);
                db.SaveChanges();
                Session["uyeid"] = uye.UyeId;
                Session["kullaniciadi"] = uye.KullaniciAdi;
                Session["yetkiid"] = uye.YetkiId;

                return RedirectToAction("Index", "Home");

            }
            return View(uye);
        }

        

    }
}