using OurReddit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

namespace OurReddit.Controllers
{
    public class CategoryController : Controller
    {
        private Models.AppContext db = new Models.AppContext();
        // GET: Categories
        public ActionResult Index()
        {
            var categories = from category in db.Categories
                             orderby category.Name
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        // GET: Category
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            var subiecte = from subiect in category.Subjects select subiect;
            ViewBag.Subjects = subiecte;
            return View();
        }

        // POST: vezi formul
        public ActionResult New()
        {

            return View();
        }
        //POST: new category
        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch (Exception e)
            {
                return View();
            }
        }
        // sa primesti datele de editat, cred
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }

        public ActionResult AddSubject(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }
        //Add Subiect to categorie
        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            try {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return Redirect("/Category/Show/" + subject.CategoryId);
                return View();
            } catch (Exception e)
            {
                ViewBag.CategoryId = subject.CategoryId;
                return View();
            }
           
        }
        //PUT: edit student
        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
           // try
            {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    var now = DateTime.Now;
                   // category.CreationDate = now;
                    category.Name = requestCategory.Name;
                    //category.UserId = requestCategory.UserId;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            } //catch (Exception e)
           // {
            //    System.Console.Error.WriteLine(e);
           //     return View();
           // }
        }
        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}