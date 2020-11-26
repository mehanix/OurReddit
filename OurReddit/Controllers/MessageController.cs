using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OurReddit.ActionFilters;
using OurReddit.Models;

namespace OurReddit.Controllers
{
    public class MessageController : Controller
    {
        private readonly Models.AppContext db = new Models.AppContext();
     
        [HttpPost]
        public ActionResult New(Message message)
        {
            try
            {
                db.Messages.Add(message);
                db.SaveChanges();
                TempData["Alert"] = "New message added.";
                return Redirect("/Subject/Show/" + message.SubjectId);
            }
            catch (Exception e)
            {
                TempData["Alert"] = "Failed to add message: " + e.Message;
                return Redirect("/Subject/Show/" + message.SubjectId);
            }
        }

        [HttpGet]
        [AlertFilter]
        public ActionResult Edit(int id)
        {
            Message message = db.Messages.Find(id);
            ViewBag.Subject = message;
            return View(message);
        }

        [HttpPut]
        public ActionResult Edit(int id, Message requestMessage)
        {
            System.Diagnostics.Debug.WriteLine(requestMessage.Content);
            try
            {
                Message message = db.Messages.Find(id);
                message.Content = requestMessage.Content;
                message.Edited = true;
                db.SaveChanges();

                TempData["Alert"] = "Message Edited.";
                return Redirect("/Subject/Show/" + message.SubjectId);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("exception");
                TempData["Alert"] = "Failed to edit subject with error: " + e.Message;
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            Message message = db.Messages.Find(id);
            db.Messages.Remove(message);
            db.SaveChanges();
            TempData["Alert"] = "Message Deleted";
            return Redirect("/Subject/Show/" + message.SubjectId);
        }
    }
}