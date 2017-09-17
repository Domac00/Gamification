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

            //ukupni kvizovi riješeni od korisnika
            int LevelOneUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 1).Count();
            int LevelTwoUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 2).Count();
            int LevelThreeUser = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizLevel == 3).Count();

            //riješeni kvizovi po kategorijama
            int LevelOneUserHTML = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 1).Where(q => q.QuizLevel == 1).Count();
            int LevelOneUserJS = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 1).Count();
            int LevelOneUserServer = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 1).Count();


            int LevelTwoUserHTML = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 1).Where(q => q.QuizLevel == 1).Count();
            int LevelTwoUserJS = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 1).Count();
            int LevelTwoUserServer = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 1).Count();


            int LevelThreeUserHTML = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 1).Where(q => q.QuizLevel == 1).Count();
            int LevelThreeUserJS = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 1).Count();
            int LevelThreeUserServer = db.UserQuizData.Find(User.Identity.GetUserId()).Quiz.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 1).Count();


            //ukupan broj kvizova po kategorijama i levelima
            int levelOneHTML = db.Quizes.Where(q => q.QuizLevel == 1).Where(q => q.QuizCategoryId == 1).Count();
            int levelTwoHTML = db.Quizes.Where(q => q.QuizLevel == 2).Where(q => q.QuizCategoryId == 1).Count();
            int levelThreeHTML = db.Quizes.Where(q => q.QuizLevel == 3).Where(q => q.QuizCategoryId == 1).Count();

            int levelOneJS = db.Quizes.Where(q => q.QuizLevel == 1).Where(q => q.QuizCategoryId == 2).Count();
            int levelTwoJS = db.Quizes.Where(q => q.QuizLevel == 2).Where(q => q.QuizCategoryId == 2).Count();
            int levelThreeJS = db.Quizes.Where(q => q.QuizLevel == 3).Where(q => q.QuizCategoryId == 2).Count();

            int levelOneServer = db.Quizes.Where(q => q.QuizLevel == 1).Where(q => q.QuizCategoryId == 3).Count();
            int levelTwoSeerver = db.Quizes.Where(q => q.QuizLevel == 2).Where(q => q.QuizCategoryId == 3).Count();
            int levelThreeServer = db.Quizes.Where(q => q.QuizLevel == 3).Where(q => q.QuizCategoryId == 3).Count();


            ViewBag.LevelHTML = 1;
            ViewBag.LevelJS = 1;
            ViewBag.LevelServer = 1;

            if (LevelOneUserHTML == levelOneHTML)
            {
                ViewBag.LevelHTML = 2;
            }
            if (LevelTwoUserHTML == levelTwoHTML)
            {
                ViewBag.LevelHTML = 3;
            }
            if (LevelOneUserJS == levelOneJS)
            {
                ViewBag.LevelJS = 2;
            }
            if (LevelTwoUserJS == levelTwoJS)
            {
                ViewBag.LevelJS = 3;
            }
            if (LevelOneUserServer == levelOneServer)
            {
                ViewBag.LevelServer = 2;
            }
            if (LevelTwoUserServer == levelTwoSeerver)
            {
                ViewBag.LevelServer = 3;
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
            var UserLevelOld = uqd.UserLevel;
           
            //postotak rezultata i prosjecni postotak korisnika
            ViewBag.NumberOfQuestions = NumberOfQuestions;
            us.Percentage = (us.score / NumberOfQuestions) * 100;
            us.Percentage = (float)Math.Round(us.Percentage,2);

            if (addScore) {
                uqd.Accuracy = (uqd.Accuracy + us.Percentage) / uqd.NumberOfSolvedQuizes;
                uqd.Accuracy = (float)Math.Round(uqd.Accuracy, 2);

                //označavanje kviza kao riješenog
                db.UserQuizData.Include("Quiz").FirstOrDefault(u => u.UserId == UserId).Quiz.Add(db.Quizes.Find(us.QuizId));
                //provjera titule
                uqd.Title = CheckTitle(uqd);
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


                    uqd.UserLevel = uqd.checkUserLevel(uqd.xp);
                    db.UserQuizData.AddOrUpdate(uqd);
                    db.SaveChanges();

                    aId = CheckAchievement(uqd, us);
                }

                

                //poruka ako se korisnku poveca level
                if (UserLevelOld != uqd.UserLevel)
                {
                    UserScore.msg = "Level " + uqd.UserLevel;
                }

                //poruka za level
                ViewBag.LevelMsg = UserScore.msg;
                

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
        public ActionResult HighScore(int orderBy)
        {
            ViewBag.UserId = User.Identity.GetUserId();

            if(orderBy == 2)
            {
                return View(db.UserQuizData.OrderByDescending(o => o.NumberOfAchievements).ToList());
            }
            else
            {

                return View(db.UserQuizData.OrderByDescending(o => o.xp).ToList());
            }
           
        }

        //Prikaz profla određenog korisnika
        public ActionResult ShowProfile(string id)
        {
            var profile = db.UserQuizData.Find(id);

            ViewBag.NumberOfAchievements = db.Achievement.Count();
            ViewBag.NumberOfHTML = db.UserQuizData.Find(id).Quiz.Where(q => q.QuizCategoryId == 1).Count();
            ViewBag.NumberOfJS = db.UserQuizData.Find(id).Quiz.Where(q => q.QuizCategoryId == 2).Count();
            ViewBag.NumberOfServer = db.UserQuizData.Find(id).Quiz.Where(q => q.QuizCategoryId == 3).Count();

            if (profile.UserLevel == 1) { ViewBag.NextLevel = 10; }
            else if (profile.UserLevel == 2) { ViewBag.NextLevel = 30; }
            else if (profile.UserLevel == 3) { ViewBag.NextLevel = 50; }
            else if (profile.UserLevel == 4) { ViewBag.NextLevel = 90; }
            else if (profile.UserLevel == 5) { ViewBag.NextLevel = 130; }
            else if (profile.UserLevel == 6) { ViewBag.NextLevel = 180; }
            else if (profile.UserLevel == 7) { ViewBag.NextLevel = 250; }
            else if (profile.UserLevel == 8) { ViewBag.NextLevel = 320; }
            else if (profile.UserLevel == 9) { ViewBag.NextLevel = 400; }
            else if (profile.UserLevel == 10) { ViewBag.NextLevel = 500; }


            // za progresbar xp
            var width = (decimal)profile.xp / (decimal)ViewBag.NextLevel;
            width *= 100;
            width = Math.Round((decimal)width, 0);
            width.ToString();
            string style = "width:" + width + "%";
            ViewBag.style = style;

            //progresbar trofeji
            var widthAch = (decimal)profile.Achievement.Count / (decimal)ViewBag.NumberOfAchievements;
            widthAch *= 100;
            widthAch = Math.Round((decimal)widthAch, 0);
            widthAch.ToString();
            string styleAch = "width:" + widthAch + "%";
            ViewBag.styleAch = styleAch;


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
            //level 3 100% -napredni
            else if (us.QuizLevel == 3 && us.Percentage == 100 && !uqd.Achievement.Any(a => a.Id == 1014))
            {
                return 1014;
            }
            //Riješeni svi level 3 kvizovi - napredni
            else if (uqd.Quiz.Where(q => q.QuizLevel == 3).Count() == LevelThreeQuiz && !uqd.Achievement.Any(a => a.Id == 1015))
            {
                return 1015;
            }
            //Level 1 100% - osnovni
            else if (us.QuizLevel==1 && us.Percentage==100 && !uqd.Achievement.Any(a => a.Id == 1016))
            {
                return 1016;
            }
            //Prosječna uspješnost nakon 5 kvizova 100% - napredni
            else if (uqd.NumberOfSolvedQuizes==5 && uqd.Accuracy==100 && !uqd.Achievement.Any(a => a.Id == 1017))
            {
                return 1017;
            }
            //Prosječna uspješnost nakon 5 kvizova veća od 60% - osnovni
            else if (uqd.NumberOfSolvedQuizes == 5 && uqd.Accuracy > 60 && !uqd.Achievement.Any(a => a.Id == 1018))
            {
                return 1018;
            }
            //Prosječna uspješnost nakon 10 kvizova veća od 60% - napredni
            else if (uqd.NumberOfSolvedQuizes == 10 && uqd.Accuracy > 60 && !uqd.Achievement.Any(a => a.Id == 1019))
            {
                return 1019;
            }

            else return 0;
        }


        //provjera titule
        private string CheckTitle(UserQuizData uqd)
        {
            
            var numberOfHTML = db.Quizes.Where(q => q.QuizCategoryId == 1).Count();
            var numberOfJS = db.Quizes.Where(q => q.QuizCategoryId == 1).Count();
            var numberOfServer = db.Quizes.Where(q => q.QuizCategoryId == 1).Count();
            var numberOfQuizes = db.Quizes.Count();
            var numberOfHTML_level1 = db.Quizes.Where(q => q.QuizCategoryId == 1).Where(q => q.QuizLevel == 1).Count();
            var numberOfHTML_level2 = db.Quizes.Where(q => q.QuizCategoryId == 1).Where(q => q.QuizLevel == 2).Count();
            var numberOfJS_level1 = db.Quizes.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 1).Count();
            var numberOfJS_level2 = db.Quizes.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 2).Count();
            var numberOfServer_level1 = db.Quizes.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 1).Count();
            var numberOfServer_level2 = db.Quizes.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 2).Count();


            if (uqd.Quiz.Count == numberOfQuizes)
            {
                
                return "Web Stručnjak";
            }
            else if(uqd.Quiz.Where(q=>q.QuizCategoryId==1).Count() == numberOfHTML)
            {
                return "HTML Stručnjak";
            }
           else if (uqd.Quiz.Where(q => q.QuizCategoryId == 2).Count() == numberOfJS)
            {
                return "JavaScript Stručnjak";
            }
           else if (uqd.Quiz.Where(q => q.QuizCategoryId == 3).Count() == numberOfServer)
            {
                return "Server Side Stručnjak";
            }
            if(uqd.Quiz.Where(q=>q.QuizCategoryId==1).Where(q=>q.QuizLevel==1).Count() == numberOfHTML_level1)
            {
                return "HTML Znalac";
            }
            if (uqd.Quiz.Where(q => q.QuizCategoryId == 2).Where(q => q.QuizLevel == 1).Count() == numberOfJS_level1)
            {
                return "JavaScript Znalac";
            }
            if (uqd.Quiz.Where(q => q.QuizCategoryId == 3).Where(q => q.QuizLevel == 1).Count() == numberOfServer_level1)
            {
                return "ServerSide Znalac";
            }
            else  if(uqd.xp > 100)
            {
                return "Web znalac";
            }
          else  if(uqd.xp > 30)
            {
                return "Web Početnik";
            }else { return "Početnik"; }

            
        }


    }


}
