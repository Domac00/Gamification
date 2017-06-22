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
            ViewBag.xp = xp;
            ViewBag.UserId = User.Identity.GetUserId();
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
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quizs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Quiz quiz)
        {
            var userId = User.Identity.GetUserId();
            quiz.UserId = userId;
            quiz.SumOfGrades = 0;
            quiz.Rating = 0;
            quiz.NumberOfRatings = 0;
            if (ModelState.IsValid)
            {   //Dodavanje kviza u bazu
                db.Quizes.Add(quiz);
                db.SaveChanges();
                return RedirectToAction("Create", "Questions", new { id = quiz.Id });
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
                var UserId = User.Identity.GetUserId();
                var QuizLevel = quiz.QuizLevel;
                UserScore.QuizLevel = QuizLevel;
                UserScore.NumberOfQuestions = quiz.Questions.Count;

                uqd.xp = uqd.xp + QuizLevel + (int)UserScore.score;
                uqd.NumberOfSolvedQuizes++;

            

                //Spremanje u bazu
                db.UserQuizData.AddOrUpdate(uqd);
                db.SaveChanges();

                return RedirectToAction("Score",UserScore);
            }

            return View(vm);
        }

        //Prikaz rezultata
        public ActionResult Score(UserScore us )
        {
            UserQuizData uqd = db.UserQuizData.Find(User.Identity.GetUserId());
            Achievement achievement = new Achievement();
            var UserId = User.Identity.GetUserId();
            var NumberOfQuestions = db.Quizes.Find(us.QuizId).Questions.Count;

            ViewBag.NumberOfQuestions = NumberOfQuestions;
            us.Percentage = (us.score / NumberOfQuestions) * 100;

            //označavanje kviz akao riješenog
            db.UserQuizData.Include("Quiz").FirstOrDefault(u => u.UserId == UserId).Quiz.Add(db.Quizes.Find(us.QuizId));
            db.UserQuizData.AddOrUpdate(uqd);
            db.SaveChanges();

            //Provjera achievementa
            var aId = achievement.CheckAchievement(uqd,us);        //id ostvarenog trofeja
            while (aId != 0)
            {
                
                //za prijavljenog korisnika dodaj trofej pomocu Id-a trofeja
                db.UserQuizData.Include("Achievement").FirstOrDefault(u => u.UserId == UserId).Achievement.Add(db.Achievement.Find(aId));
                var msg = db.Achievement.Find(aId).Description.ToString();
                db.UserQuizData.AddOrUpdate(uqd);
                db.SaveChanges();
                ViewBag.message = msg;
                 aId = achievement.CheckAchievement(uqd, us);
            }

            return View(us);
        }

        //Post Score
        [HttpPost]
        [ActionName("Score")]
        public ActionResult ScorePost(UserScore user)
        {
            // izračun ocjene kviza
            var quiz = db.Quizes.Find(user.QuizId);
            var suma = quiz.SumOfGrades;
            var NumberOfRatings = quiz.NumberOfRatings + 1;
            suma = suma + user.Rate;

            quiz.SumOfGrades = suma;
            quiz.NumberOfRatings = NumberOfRatings;
            quiz.Rating = suma / NumberOfRatings;

            db.Quizes.AddOrUpdate(quiz);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // high score
        public ActionResult HighScore()
        {
            
            return View(db.UserQuizData.OrderByDescending(o => o.xp).ToList());
        }
    }


}
