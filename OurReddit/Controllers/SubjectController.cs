using OurReddit.ActionFilters;
using OurReddit.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurReddit.Controllers
{
    public class SubjectController : Controller
    {
        private Models.ApplicationDbContext db = new Models.ApplicationDbContext();
        
        [HttpGet]
        [AlertFilter]
        public ActionResult Show(int id)
        {
            Subject subject = db.Subjects.Find(id);
            ViewBag.subject = subject;
            return View();
        }

        [HttpGet]
        public ActionResult New(int id)
        {
            ViewBag.CategoryId = id;
            return View();
        }

        [HttpPost]
        public ActionResult New(Subject subject)
        {
            try
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                TempData["Alert"] = "Created new subject: " + subject.Title.ToString();
                return Redirect("/Category/Show/" + subject.CategoryId);
            }
            catch (Exception e)
            {
                TempData["Alert"] = "Failed to create subject with: " + e.Message;
                return Redirect("/Category/Show/" + subject.CategoryId);
            }
        }

        [HttpGet]
        [AlertFilter]
        public ActionResult Edit(int id)
        {
            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
            return View(subject);
        }

        [HttpPut]
        public ActionResult Edit(int id, Subject requestSubject)
        {
            try
            {
                Subject subject = db.Subjects.Find(id);
                if (TryUpdateModel(subject))
                {
                    subject.Title = requestSubject.Title;
                    subject.Description = requestSubject.Description;
                    db.SaveChanges();
                    TempData["Alert"] = "Edited subject: " + subject.Title.ToString();
                }
                else
                {
                    TempData["Alert"] = "Failed to edit subject: " + subject.Title.ToString();
                    return View(subject);
                }
                return Redirect("/Category/Show/" + subject.CategoryId);
            }
            catch (Exception e)
            {
                TempData["Alert"] = "Failed to edit subject with error: " + e.Message;
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            TempData["Alert"] = "Deleted subject: " + subject.Title.ToString();
            return Redirect("/Category/Show/" + subject.CategoryId);
        }
    }
}