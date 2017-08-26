using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Gamification;
using Gamification.Models;
using Microsoft.AspNet.Identity;

namespace Gamification.Controllers
{
    public class TutorialController : Controller
    {
        private Context db = new Context();

        // GET: Tutorial
        public ActionResult Index()
        {
            return View(db.Tutorial.ToList());
        }

        // GET: Tutorial/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorial.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // GET: Tutorial/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tutorial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,isCompleted")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Tutorial.Add(tutorial);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tutorial);
        }

        // GET: Tutorial/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorial.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,isCompleted")] Tutorial tutorial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorial).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutorial);
        }

        // GET: Tutorial/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tutorial tutorial = db.Tutorial.Find(id);
            if (tutorial == null)
            {
                return HttpNotFound();
            }
            return View(tutorial);
        }

        // POST: Tutorial/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tutorial tutorial = db.Tutorial.Find(id);
            db.Tutorial.Remove(tutorial);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Tutorials()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View(db.Tutorial.ToList());
        }

        
        public ActionResult TutorialCompleted(int id)
        {
            var user=db.UserQuizData.Find(User.Identity.GetUserId());
            var tut = db.Tutorial.Find(id);
            var UserId = User.Identity.GetUserId();
            tut.isCompleted = true;
            user.xp += 10;

            db.UserQuizData.Include("Tutorial").FirstOrDefault(u => u.UserId == UserId).Tutorial.Add(tut);
            db.SaveChanges();
            return RedirectToAction("Tutorials");
        }
    }
}
