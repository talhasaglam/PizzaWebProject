using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaWebProje.Models;
using System.Web.Helpers;
using System.IO;

namespace PizzaWebProje.Controllers
{
    public class AdminPizzaController : Controller
    {

        PizzaSitesiDB db = new PizzaSitesiDB();
        // GET: AdminPizza
        public ActionResult Index()
        {
            var pizzas = db.Pizza.ToList();
            return View(pizzas);

        }

        // GET: AdminPizza/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminPizza/Create
        public ActionResult Create()
        {
            ViewData["PizzaId"] = db.Pizza;
            return View();
        }

        // POST: AdminPizza/Create
        [HttpPost]
        public ActionResult Create(Pizza pizza, HttpPostedFileBase Fotograf)
        {

            if (ModelState.IsValid)

            {
                if (Fotograf != null)
                {
                    WebImage img = new WebImage(Fotograf.InputStream);
                    FileInfo fotoinfo = new FileInfo(Fotograf.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(516, 450);
                    img.Save("~/Content/images/Home" + newfoto);
                    pizza.Fotograf = "/Content/images/Home" + newfoto;
                   

                }

                db.Pizza.Add(pizza);
                db.SaveChanges();

                return RedirectToAction("Index");


            }
            return View(pizza);


        }
  

        // GET: AdminPizza/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pizza = db.Pizza.Where(x => x.PizzaId == id).SingleOrDefault();
            if (pizza == null)
            {
                return HttpNotFound();
            }

            return View(pizza);
        }

        // POST: AdminPizza/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, HttpPostedFile Fotograf, Pizza pizza)
        {
            try
            {
                var pizzaDuzenle = db.Pizza.Where(x => x.PizzaId == id).SingleOrDefault();

                if (Fotograf != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(pizzaDuzenle.Fotograf)))
                    {

                        System.IO.File.Delete(Server.MapPath(pizzaDuzenle.Fotograf));
                    }

                    WebImage img = new WebImage(Fotograf.InputStream);
                    FileInfo fotoinfo = new FileInfo(Fotograf.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(516, 450);
                    img.Save("~/Content/images/Home" + newfoto);
                    pizzaDuzenle.Fotograf = "/Content/images/Home" + newfoto;

                    pizzaDuzenle.PizzaAdi = pizza.PizzaAdi;
                    pizzaDuzenle.Fiyat = pizza.Fiyat;

                    db.SaveChanges();
                    return RedirectToAction("Index");

                }

                return View();
            }
            catch
            {
                return View();
            }
      
        }

        // GET: AdminPizza/Delete/5
        public ActionResult Delete(int id)
        {
            var pizza = db.Pizza.Where(x => x.PizzaId == id).SingleOrDefault();
            if (pizza == null)
            {
                return HttpNotFound();
            }

            return View(pizza);
        }

        // POST: AdminPizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var pizzaSil = db.Pizza.Where(x => x.PizzaId == id).SingleOrDefault();
                if (pizzaSil == null)
                {
                    return HttpNotFound();
                }

               
                db.Pizza.Remove(pizzaSil);
                db.SaveChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
