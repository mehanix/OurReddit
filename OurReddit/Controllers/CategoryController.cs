﻿using Microsoft.AspNet.Identity;
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
        private readonly Models.ApplicationDbContext db = new Models.ApplicationDbContext();

        [HttpGet]
        [AlertFilter]
        //oricine poate vedea categoriile
        public ActionResult Index()
        {
            SetAccessRights();
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
            SetAccessRights();
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View();
        }
        
        [HttpGet]
        [AlertFilter]
        [Authorize(Roles = "Admin")]
        public ActionResult New()
        {
            SetAccessRights();
            return View();
        }

        [HttpPost]
        // doar admin poate crea categorie noua
        [Authorize(Roles="Admin")]
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
        // doar admin poate modifica categoriile
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            SetAccessRights();
            Category category = db.Categories.Find(id);
            ViewBag.Category = category;
            return View(category);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        // doar admin poate modifica categoriile
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
        // doar admin poate sterge categoriile
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            TempData["Alert"] = "Deleted category: " + category.Name.ToString();
            return RedirectToAction("Index");
        }

        [NonAction]
        private void SetAccessRights()
        {
            ViewBag.isAdmin = User.IsInRole("Admin");
            ViewBag.isModerator = User.IsInRole("Moderator");
            ViewBag.currentUserId = User.Identity.GetUserId();
        }
    }
}