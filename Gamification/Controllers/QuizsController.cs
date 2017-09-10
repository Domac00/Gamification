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

            int LevelOneUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 1).Count();
            int LevelTwoUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 2).Count();
            int LevelThreeUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 3).Count();

            int levelOne =db.Quizes.Where(q => q.QuizLevel == 1).Count();
            int levelTwo = db.Quizes.Where(q => q.QuizLevel == 2).Count();
            int levelThree = db.Quizes.Where(q => q.QuizLevel == 3).Count();
            ViewBag.Level = 1;

            if (LevelOneUser == levelOne)
            {
                ViewBag.Level = 2;
            }
            if (LevelTwoUser == levelTwo)
            {
                ViewBag.Level = 3;
            }
          


            return View(db.QuizCategory.ToList());
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
            ViewBag.QuizCategoryId = new SelectList(db.QuizCategory, "Id", "Name");
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
        public ActionResult StartQuiz(int id,bool addScore)
        {

            Quiz quiz = db.Quizes.Find(id);
            Question question = db.Questions.First(q => q.QuizId == quiz.Id);
            QuizViewModel vm = new QuizViewModel();

            vm.Name = quiz.Name;
            vm.Question = question.Text;
            vm.Answers = question.Answers;
            vm.QuestionId = question.Id;
            vm.addScore = addScore;

            Session["UserScore"] = new UserScore();
            var us = Session["UserScore"] as UserScore;
            us.addScore = addScore;
            Session["UserScore"] = us;

            return View(vm);

        }

        //POST Pokretanje Kviza
        [HttpPost]
        public ActionResult StartQuiz(QuizViewModel vm)
        {
           
            
            Answer answer = db.Answers.Find(vm.AnswerId);
            //provjera odgovora
            var UserScore = Session["UserScore"] as UserScore;
            bool addScore = UserScore.addScore;


            UserScore.QuizId = db.Questions.Find(answer.QuestionId).QuizId;


            if (answer.IsTrue)
            {
                UserScore.score++;
                UserScore.UserAnswers.Add(vm.AnswerId);
                Session["UserScore"] = UserScore;
            }else
            {
                UserScore.UserAnswers.Add(vm.AnswerId);
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
                var UserLevelOld = uqd.UserLevel;


                UserScore.QuizLevel = QuizLevel;
                UserScore.NumberOfQuestions = quiz.Questions.Count;

                if (addScore) {
                    uqd.xp = uqd.xp + ((int)UserScore.score) * QuizLevel;
                    uqd.NumberOfSolvedQuizes++;
                    uqd.UserLevel = uqd.checkUserLevel(uqd.xp);

                    //Spremanje u bazu
                    db.UserQuizData.AddOrUpdate(uqd);
                    db.SaveChanges();

                    //poruka ako se korisnku poveca level
                    if (UserLevelOld != uqd.UserLevel)
                    {
                        UserScore.msg = "Level " + uqd.UserLevel;
                    }
                }
               
            

               

                return RedirectToAction("Score",UserScore);
            }

            return View(vm);
        }

        //Prikaz rezultata
        public ActionResult Score(UserScore us  )
        {
            var UserScore = Session["UserScore"] as UserScore;
            bool addScore = UserScore.addScore;
            us.UserAnswers = UserScore.UserAnswers;
            us.addScore = addScore;
            UserQuizData uqd = db.UserQuizData.Find(User.Identity.GetUserId());
            Achievement achievement = new Achievement();
            var UserId = User.Identity.GetUserId();
            var xpToShow = us.score * us.QuizLevel;
            var NumberOfQuestions = db.Quizes.Find(us.QuizId).Questions.Count;
           
            //postotak rezultata i prosjecni postotak korisnika
            ViewBag.NumberOfQuestions = NumberOfQuestions;
            us.Percentage = (us.score / NumberOfQuestions) * 100;
            us.Percentage = (float)Math.Round(us.Percentage,2);

            if (addScore) {
                uqd.Accuracy = (uqd.Accuracy + us.Percentage) / uqd.NumberOfSolvedQuizes;

                //označavanje kviza kao riješenog
                db.UserQuizData.Include("Quiz").FirstOrDefault(u => u.UserId == UserId).Quiz.Add(db.Quizes.Find(us.QuizId));
                db.UserQuizData.AddOrUpdate(uqd);
                db.SaveChanges();


                //Provjera achievementa
                var aId = CheckAchievement(uqd, us);        //id ostvarenog trofeja
                while (aId != 0)
                {

                    //za prijavljenog korisnika dodaj trofej pomocu Id-a trofeja
                    db.UserQuizData.Include("Achievement").FirstOrDefault(u => u.UserId == UserId).Achievement.Add(db.Achievement.Find(aId));
                    uqd.NumberOfAchievements++;
                    //xp zbog osvojenog trofeja
                    uqd.xp += 10;
                    xpToShow += 10;

                    //poruka za trofej
                    var AchievementName = db.Achievement.Find(aId).Name.ToString();
                    var AchievementMsg = db.Achievement.Find(aId).Description.ToString();
                    ViewBag.message = AchievementName + " - " + AchievementMsg;

                    db.UserQuizData.AddOrUpdate(uqd);
                    db.SaveChanges();

                    aId = CheckAchievement(uqd, us);
                }

                //poruka za level
                ViewBag.LevelMsg = us.msg;
                

            }


            ViewBag.xpToShow = xpToShow;




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
            ViewBag.UserId = User.Identity.GetUserId();
           
            return View(db.UserQuizData.OrderByDescending(o => o.xp).ToList());
        }

        //Prikaz profla određenog korisnika
        public ActionResult ShowProfile(string id)
        {
            var profile = db.UserQuizData.Find(id);
            return View(profile);
        }

        //prikaz odgovora nakon kviza
        public ActionResult ShowAnswers(string UserAnswers,  int QuizId)
        {
            List<int> ua = System.Web.Helpers.Json.Decode<List<int>>(UserAnswers);
            var quiz = db.Quizes.Find(QuizId);
            

            ViewBag.UserAnswers = ua;
            

            return View(quiz);
        }

        //ponovljeni kvozovi
        public ActionResult CompletedQuizzes()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            return View(db.QuizCategory.ToList());
        }


        //Funkcija za provjeru trofeja
        private int CheckAchievement(UserQuizData uqd, UserScore us)
        {
            var LevelOneQuiz = db.Quizes.Where(q => q.QuizLevel == 1).Count();
            var LevelTwoQuiz = db.Quizes.Where(q => q.QuizLevel == 2).Count();
            var LevelThreeQuiz = db.Quizes.Where(q => q.QuizLevel == 3).Count();


            //Prvi kviz odigran
            if (uqd.NumberOfSolvedQuizes == 1 && !uqd.Achievement.Any(a => a.Id == 6)) { return 6; }
            //5 rjesenih kvizova
            else if (uqd.NumberOfSolvedQuizes == 5 && !uqd.Achievement.Any(a => a.Id == 7)) { return 7; }
            //10 kvizova
            else if (uqd.NumberOfSolvedQuizes == 10 && !uqd.Achievement.Any(a => a.Id == 8)) { return 8; }
            //30 Kvizova
            else if (uqd.NumberOfSolvedQuizes == 30 && !uqd.Achievement.Any(a => a.Id == 9)) { return 9; }
            //level 2 100%
            else if (us.QuizLevel == 2 && us.Percentage == 100 && !uqd.Achievement.Any(a => a.Id == 10)) { return 10; }
            //Rješeni svi level 1 kvizovi
            else if(uqd.Quiz.Where(q=>q.QuizLevel==1).Count() == LevelOneQuiz && !uqd.Achievement.Any(a => a.Id == 11)){
                return 11; }
            //Riješeni svi level 2 kvizovi
            else if (uqd.Quiz.Where(q => q.QuizLevel == 2).Count() == LevelTwoQuiz && !uqd.Achievement.Any(a => a.Id == 12))
            {
                return 12;
            }
            //Riješeni svi level 3 kvizovi
            else if (uqd.Quiz.Where(q => q.QuizLevel == 3).Count() == LevelThreeQuiz && !uqd.Achievement.Any(a => a.Id == 13))
            {
                return 13;
            }
            else return 0;
        }


    }


}
