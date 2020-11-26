using OurReddit.ActionFilters;
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
        private readonly Models.AppContext db = new Models.AppContext();

        [HttpGet]
        [AlertFilter]
        public ActionResult Index()
        {
            var categories = from category in db.Categories
                             orderby category.Name
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        [HttpGet]
        [AlertFilter]
        public ActionResult Show(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }
        
        [HttpGet]
        [AlertFilter]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                TempData["Alert"] = "Created new category: " + category.Name.ToString();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Alert"] = "Failed to create category with: " + e.Message;
                return View();
            }
        }
        
        [HttpGet]
        [AlertFilter]
        public ActionResult Edit(int id)
        {
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View(category);
        }

        [HttpPut]
        public ActionResult Edit(int id, Category requestCategory)
        {
            try
            {
                Category category = db.Categories.Find(id);
                if (TryUpdateModel(category))
                {
                    category.Name = requestCategory.Name;
                    db.SaveChanges();
                    TempData["Alert"] = "Edited category name: " + category.Name.ToString();
                }
                else
                {
                    TempData["Alert"] = "Failed to edit category: " + category.Name.ToString();
                    return View(category);
                }
                return RedirectToAction("Index");
            } 
            catch (Exception e)
            {
                TempData["Alert"] = "Failed to edit category with error: " + e.Message;
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Alert"] = "Deleted category: " + category.Name.ToString();
            return RedirectToAction("Index");
        }
    }
}