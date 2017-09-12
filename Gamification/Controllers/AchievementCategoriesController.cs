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
    public class AchievementCategoriesController : Controller
    {
        private Context db = new Context();

        // GET: AchievementCategories
        public ActionResult Index()
        {
            return View(db.AchievementCategory.ToList());
        }

        // GET: AchievementCategories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementCategory achievementCategory = db.AchievementCategory.Find(id);
            if (achievementCategory == null)
            {
                return HttpNotFound();
            }
            return View(achievementCategory);
        }

        // GET: AchievementCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AchievementCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] AchievementCategory achievementCategory)
        {
            if (ModelState.IsValid)
            {
                if(achievementCategory.Id == 1)
                {
                    achievementCategory.ImageUrl = "TrofejOsnovni.png";
                }
                if (achievementCategory.Id == 2)
                {
                    achievementCategory.ImageUrl = "TrofejNapredni.png";
                }
                db.AchievementCategory.Add(achievementCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(achievementCategory);
        }

        // GET: AchievementCategories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementCategory achievementCategory = db.AchievementCategory.Find(id);
            if (achievementCategory == null)
            {
                return HttpNotFound();
            }
            return View(achievementCategory);
        }

        // POST: AchievementCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] AchievementCategory achievementCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(achievementCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(achievementCategory);
        }

        // GET: AchievementCategories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementCategory achievementCategory = db.AchievementCategory.Find(id);
            if (achievementCategory == null)
            {
                return HttpNotFound();
            }
            return View(achievementCategory);
        }

        // POST: AchievementCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AchievementCategory achievementCategory = db.AchievementCategory.Find(id);
            db.AchievementCategory.Remove(achievementCategory);
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
