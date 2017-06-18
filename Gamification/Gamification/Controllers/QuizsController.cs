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
using System.Data.Entity.Migrations;

namespace Gamification.Controllers
{
    public class QuizsController : Controller
    {
        private Context db = new Context();
        

        // GET: Quizs
        public ActionResult Index()
        {
            var xp = db.UserQuizData.Find(User.Identity.GetUserId()).xp;
            ViewBag.xp = xp.ToString();
            return View(db.Quizes.ToList());
        }

        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Quiz quiz)
        {
            var userId = User.Identity.GetUserId();
            quiz.UserId = userId;
            if (ModelState.IsValid)
            {
                db.Quizes.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizes.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizes.Find(id);
            db.Quizes.Remove(quiz);
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

        //Pokretanje kviza
        public ActionResult StartQuiz(int id)
        {

            Quiz quiz = db.Quizes.Find(id);
            Question question = db.Questions.First(q => q.QuizId == quiz.Id);

            QuizViewModel vm = new QuizViewModel();
            vm.Name = quiz.Name;
            vm.Question = question.Text;
            vm.Answers = question.Answers;
            
            vm.QuestionId = question.Id;

            Session["UserScore"] = new UserScore();
            
            return View(vm);

        }

        //POST Pokretanje Kviza
        [HttpPost]
        public ActionResult StartQuiz(QuizViewModel vm)
        {
            
            Answer answer = db.Answers.Find(vm.AnswerId);
            //provjera odgovora
            var UserScore = Session["UserScore"] as UserScore;
            UserScore.QuizId = db.Questions.Find(answer.QuestionId).QuizId;
            if (answer.IsTrue)
            {
                UserScore.score++;
                Session["UserScore"] = UserScore;
            }

            //Iduce pitanje
            Question question = db.Questions.Find(answer.QuestionId+1);
            
            if (question != null && question.QuizId==UserScore.QuizId)
            {
                
                vm.Question = question.Text;
                vm.Answers = question.Answers;
                vm.QuestionId = question.Id;
            }else
            {
                //dodavanje xp-a i broj rijesenih kvizova
                UserQuizData uqd = db.UserQuizData.Find(User.Identity.GetUserId());
                Quiz quiz = db.Quizes.Find(UserScore.QuizId);
                var QuizLevel = quiz.QuizLevel;

                uqd.xp = uqd.xp + QuizLevel;
                uqd.NumberOfSolvedQuizes++;

                //Spremanje u bazu
                db.UserQuizData.AddOrUpdate(uqd);
                db.SaveChanges();

                return RedirectToAction("Score",UserScore);
            }

            return View(vm);
        }

        //Prikaz rezultata
        public ActionResult Score(UserScore us)
        {
            ViewBag.NumberOfQuestions = db.Quizes.Find(us.QuizId).Questions.Count;
            
            return View(us);
        }

        // high score
        public ActionResult HighScore()
        {
            
            return View(db.UserQuizData.OrderBy(o=>o.xp).ToList());
        }
    }


}
