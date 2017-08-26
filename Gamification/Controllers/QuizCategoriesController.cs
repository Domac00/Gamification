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

namespace Gamification.Controllers
{
    public class QuizCategoriesController : Controller
    {
        private Context db = new Context();

        // GET: QuizCategories
        public ActionResult Index()
        {
            return View(db.QuizCategory.ToList());
        }

        // GET: QuizCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizCategory quizCategory = db.QuizCategory.Find(id);
            if (quizCategory == null)
            {
                return HttpNotFound();
            }
            return View(quizCategory);
        }

        // GET: QuizCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuizCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] QuizCategory quizCategory)
        {
            if (ModelState.IsValid)
            {
                db.QuizCategory.Add(quizCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quizCategory);
        }

        // GET: QuizCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizCategory quizCategory = db.QuizCategory.Find(id);
            if (quizCategory == null)
            {
                return HttpNotFound();
            }
            return View(quizCategory);
        }

        // POST: QuizCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] QuizCategory quizCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quizCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quizCategory);
        }

        // GET: QuizCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuizCategory quizCategory = db.QuizCategory.Find(id);
            if (quizCategory == null)
            {
                return HttpNotFound();
            }
            return View(quizCategory);
        }

        // POST: QuizCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuizCategory quizCategory = db.QuizCategory.Find(id);
            db.QuizCategory.Remove(quizCategory);
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
    }
}
