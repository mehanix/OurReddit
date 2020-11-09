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
        private Models.AppContext db = new Models.AppContext();
        // GET: Subjects
        public ActionResult Index()
        {
            var categories = from subject in db.Subjects
                             orderby subject.Title
                             select subject;
            ViewBag.Subjects = categories;
            return View();
        }

        // GET: Subject
        public ActionResult Show(int id)
        {
            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
            return View();
        }

        // POST: vezi formul
        public ActionResult New()
        {

            return View();
        }
        //POST: new subject
        [HttpPost]
        public ActionResult New(Subject subject)
        {
            try
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }
        // sa primesti datele de editat, cred
        public ActionResult Edit(int id)
        {
            Subject subject = db.Subjects.Find(id);
            ViewBag.Subject = subject;
            return View();
        }

        //PUT: edit student
        [HttpPut]
        public ActionResult Edit(int id, Subject requestSubject)
        {
            // try
            {
                Subject subject = db.Subjects.Find(id);
                if (TryUpdateModel(subject))
                {
                    var now = DateTime.Now;
                    // subject.CreationDate = now;
                    subject.Title = requestSubject.Title;
                    subject.Description = requestSubject.Description;
                    //subject.UserId = requestSubject.UserId;
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
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}