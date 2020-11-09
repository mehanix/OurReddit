using OurReddit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OurReddit.Controllers
{
    public class UserController : Controller
    {
        private Models.AppContext db = new Models.AppContext();

        [HttpGet]
        public String Index()
        {
            var users = from user in db.Users
                        orderby user.Name
                        select user;

            ViewBag.Users = users;
            return "yes";
        }
        
        [HttpGet]
        public ActionResult Show(int id)
        {
            User user = db.Users.Find(id);
            ViewBag.User = user;
            return View();
        }
        
        [HttpPost]
        public ActionResult New(User user)
        {
            try
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            ViewBag.User = user;
            var users = from usr in db.Users select usr;
            return View();
        }

        [HttpPut]
        public ActionResult Edit(int id, User requestUser)
        {
            try
            {
                User user = db.Users.Find(id);
                if (TryUpdateModel(user))
                {
                    user.Name = requestUser.Name;
                    user.Email = requestUser.Email;
                    user.Password = requestUser.Password;
                    user.Birthday = requestUser.Birthday;
                    user.Description = requestUser.Description;
                    user.RoleID = requestUser.RoleID;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            return View();
        }
    }
}